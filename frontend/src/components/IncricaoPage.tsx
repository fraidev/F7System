import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'
import { useParams } from 'react-router-dom'
import { getMatriculaById } from '../services/matricula-service'
import SaveIcon from '@material-ui/icons/Save'
import {
  Button,
  Dialog,
  DialogTitle,
  List,
  ListItem,
  ListItemText,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow
} from '@material-ui/core'
import IconButton from '@material-ui/core/IconButton'
import { addInscricoesMatriculaEstudante } from '../services/pessoa-service'
import { useSnackbar } from 'notistack'

const useStyles = makeStyles(() => ({
  fields: {
    margin: '10px'
  },
  cancelButton: {
    color: 'white'
  }
}))

const InscricaoPage: React.FC = () => {
  const classes = useStyles()
  const [matricula, setMatricula] = useState<any>({})
  const { estudanteId, matriculaId } = useParams()
  const [open, setOpen] = React.useState(false)
  const [selectedTurmas, setSelectedTurmas] = React.useState<any[]>([])
  const [dialogTurmas, setDialogTurmas] = React.useState<any[]>([])
  const { enqueueSnackbar } = useSnackbar()

  useEffect(() => {
    // getEstudanteById(estudanteId).then((res: any) => {
    //   setEstudante(res.data)
    // })

    getMatriculaById(matriculaId).then((res: any) => {
      setMatricula(res.data)
    })
  }, [matriculaId])

  function addDisciplina(turmas: any[]) {
    setDialogTurmas(turmas)
    setOpen(true)
  }

  const handleClose = (turma: any, disciplina: any) => {
    setOpen(false)
    if (turma?.id) {
      turma.disciplina = disciplina
      if (!selectedTurmas.find(x => x.id === turma.id)) {
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
              <Button variant="contained" color="primary" onClick={() => addDisciplina(disciplina?.turmas)}>
                escolher turmas
              </Button>
              <TurmasDialog open={open} onClose={(turma) => handleClose(turma, disciplina)}
                turmas={dialogTurmas}/>

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
    <Paper style={{ height: '80vh', marginLeft: '4vw', padding: '20px' }}>
      {/* {estudante?.nome} */}

      <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
        <div style={{ height: '40%' }}>
          <div style={{ display: 'flex', position: 'relative' }}>

            <div style={{ flex: '1' }}>Inscrições</div>

            <IconButton style={{ backgroundColor: '#2196f3', position: 'absolute', right: '5px' }} onClick={onSave}
              aria-label="add">
              <SaveIcon style={{ color: 'white' }} fontSize="large"/>
            </IconButton>

          </div>

          <ol className="planetas-list">
            {selectedTurmas.map((turma: any) => (
              <li key={turma.id}
                style={{ fontSize: '18px' }}>  {'Disciplina : ' + turma?.disciplina?.nome + ' - Sala: ' + turma?.sala + ' - Professor: ' + turma?.professor?.nome +
              turma?.horarios.reduce((acc, cur, idx) => acc + (idx === 0 ? ' ' : ' - ') +
                cur.diaDaSemana + ' ' + cur.start + ' - ' + cur.end, ' - Horarios: ')}</li>
            ))}
          </ol>
        </div>

        <div>
          <div>Disciplinas</div>
          {table}
        </div>
      </div>
      {/* {matricula?.grade?.disciplinas.map((x:any) => ( */}
      {/*  <div>{x.nome}</div> */}
      {/* ))} */}

    </Paper>
  )
}

export interface SimpleDialogProps {
  open: boolean;
  // selectedValue: string;
  onClose: (value: any | null) => void;
  turmas: any[]
}

const TurmasDialog = (props: SimpleDialogProps) => {
  const classes = useStyles()
  const { onClose, turmas, open } = props

  const handleClose = () => {
    onClose(null)
  }

  const handleListItemClick = (value: any) => {
    onClose(value)
  }

  return (
    <Dialog onClose={handleClose} aria-labelledby="simple-dialog-title" open={open}>
      <DialogTitle id="simple-dialog-title">Selecione uma turma</DialogTitle>
      <List>
        {turmas.map((turma: any) => (
          <ListItem button onClick={() => handleListItemClick(turma)} key={turma}>
            <ListItemText primary={'Sala: ' + turma?.sala + ' - Professor: ' + turma?.professor?.nome}
              secondary={turma?.horarios.reduce((acc, cur, idx) => acc + (idx === 0 ? ' ' : ' - ') + cur.diaDaSemana + ' ' + cur.start + ' - ' + cur.end, 'Horarios: ')}/>
          </ListItem>
        ))}
      </List>
    </Dialog>
  )
}

export default InscricaoPage
