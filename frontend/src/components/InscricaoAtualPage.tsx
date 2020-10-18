import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { useParams } from 'react-router-dom'
import { getMatriculaAtualById, getMatriculaById } from '../services/matricula-service'
import SaveIcon from '@material-ui/icons/Save'
import { Button, Table, TableBody, TableCell, TableHead, TableRow } from '@material-ui/core'
import IconButton from '@material-ui/core/IconButton'
import { addInscricoesMatriculaEstudante } from '../services/pessoa-service'
import { useSnackbar } from 'notistack'
import Grade from './Grade'
import { Disciplina, Matricula, Turma } from '../model/estudante-model'
import TurmasDialog from './TurmasDialog'
import DeleteIcon from '@material-ui/icons/Delete'

const InscricaoAtualPage: React.FC = () => {
  const [matricula, setMatricula] = useState<Matricula>()
  const { matriculaId } = useParams()
  const [open, setOpen] = React.useState(false)
  const [selectedTurmas, setSelectedTurmas] = React.useState<Turma[]>([])
  const [dialogTurmas, setDialogTurmas] = React.useState<any>([])
  const { enqueueSnackbar } = useSnackbar()

  useEffect(() => {
    getMatriculaAtualById(matriculaId).then((res: any) => {
      setMatricula(res.data)

      setSelectedTurmas(res.data?.inscricoes?.flatMap(x => x.turma))
    })
  }, [matriculaId])

  function addDisciplina(disciplina: Disciplina) {
    setDialogTurmas(disciplina)
    setOpen(true)
  }

  const onDelete = (turma: any) => {
    const index = selectedTurmas.findIndex(x => x.id === turma.id)
    if (index > -1) {
      selectedTurmas.splice(index, 1)

      const turmas = [...selectedTurmas]

      setSelectedTurmas([])
      setSelectedTurmas(turmas)
    }
  }

  const handleClose = (turma: Turma) => {
    setOpen(false)

    const selectedTurmasIds = selectedTurmas.flatMap(x => x.horarios.map(x => x.id))
    const turmaIds = turma?.horarios.map(x => x.id)
    let horarioIndisponivel = false

    if (turmaIds) {
      turmaIds.forEach(id => {
        if (selectedTurmasIds.includes(id)) {
          enqueueSnackbar('Horario indisponivel', { variant: 'error' })
          horarioIndisponivel = true
        }
      })

      if (!horarioIndisponivel && !selectedTurmas.find(x => x.id === turma?.id)) {
        selectedTurmas.push(turma)
        setSelectedTurmas(selectedTurmas)
      }
    }
  }

  const table = matricula?.grade?.disciplinas
    ? <Table>
      <TableHead>
        <TableRow>
          <TableCell>Nome</TableCell>
          <TableCell>Creditos</TableCell>
          <TableCell align="right">
          </TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {matricula?.grade?.disciplinas.map((disciplina: any) => (
          <TableRow key={disciplina?.id}>
            <TableCell component="th" scope="row">
              {disciplina?.nome}
            </TableCell>
            <TableCell component="th" scope="row">
              {disciplina?.creditos}
            </TableCell>
            <TableCell align="right">
              <Button variant="contained" color="primary" onClick={() => addDisciplina(disciplina)}>
                escolher turmas
              </Button>
              <TurmasDialog open={open} onClose={(turma) => handleClose(turma)}
                disciplina={dialogTurmas}/>

            </TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
    : <div></div>

  const onSave = () => {
    const cmd = {
      matriculaId,
      turmaIds: selectedTurmas.map(x => x.id)
    }
    addInscricoesMatriculaEstudante(cmd).then(() => {
      enqueueSnackbar('Inscrições adicionadas a matricula com sucesso.', { variant: 'success' })
    })
  }

  return (
    <Paper style={{ minHeight: '80vh', marginLeft: '4vw', padding: '20px' }}>
      {/* {estudante?.nome} */}

      <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
        <div>
          <div style={{ display: 'flex', position: 'relative' }}>

            <div style={{ flex: '1' }}>Inscrições desse semestre</div>

            <IconButton style={{ backgroundColor: '#2196f3', position: 'absolute', right: '5px', padding: '5px' }} onClick={onSave}
              aria-label="add">
              <SaveIcon style={{ color: 'white' }} fontSize="large"/>
            </IconButton>

          </div>

          <ol className="planetas-list">
            {selectedTurmas?.map((turma: any) => (
              <li key={turma.id}
                style={{
                  fontSize: '16px',
                  textAlign: 'start'
                }}>  {'Disciplina : ' + turma?.disciplina?.nome + ' - Sala: ' + turma?.sala + ' - Professor: ' + turma?.professor?.nome +
              turma?.horarios.reduce((acc, cur, idx) => acc + (idx === 0 ? ' ' : ' - ') +
                cur.diaDaSemana + ' ' + cur.start + ' - ' + cur.end, ' - Horarios: ')}

                <IconButton color="inherit" onClick={() => onDelete(turma)}>
                  <DeleteIcon/>
                </IconButton>
              </li>
            ))}
          </ol>

          <Grade selectedTurmas={selectedTurmas}/>
        </div>

        <div style={{ paddingTop: '20px' }}>
          <div>Disciplinas desse semestre</div>
          {table}
        </div>
      </div>
      {/* {matricula?.grade?.disciplinas.map((x:any) => ( */}
      {/*  <div>{x.nome}</div> */}
      {/* ))} */}

    </Paper>
  )
}

export default InscricaoAtualPage
