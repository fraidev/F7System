import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { useParams } from 'react-router-dom'
import { getMatriculaById } from '../services/matricula-service'
import { Matricula } from '../model/estudante-model'
import { Table, TableBody, TableCell, TableHead, TableRow, Typography } from '@material-ui/core'
import Accordion from '@material-ui/core/Accordion'
import AccordionSummary from '@material-ui/core/AccordionSummary'
import AccordionDetails from '@material-ui/core/AccordionDetails'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import _ from 'lodash'

const InscricaoTodosPage: React.FC = () => {
  const [matricula, setMatricula] = useState<Matricula>()
  const { matriculaId } = useParams()

  useEffect(() => {
    getMatriculaById(matriculaId).then((res: { data: Matricula }) => {
      setMatricula(res.data)
    })
  }, [matriculaId])

  const disciplines = () => {
    const semestreDisciplinas = _.groupBy(matricula?.grade?.semestreDisciplinas, x => x.semestre)

    const inscricoesInfoById = (id) => {
      const inscricao = matricula.inscricoes.find(x => x?.turma?.disciplina.id === id && x.completa)

      if (inscricao) {
        return {
          completa: inscricao ? (inscricao.completa ? 'Concluido' : 'Pendente') : 'Pendente',
          nota: inscricao ? inscricao.notaFinal : '-'
        }
      }
    }

    const retval = []

    for (const key in semestreDisciplinas) {
      retval.push(
        <div key={key}><
          Accordion>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon/>}
            aria-controls="panel1a-content"
            id="panel1a-header"
          >
            <Typography>{key}º Semestre</Typography>
          </AccordionSummary>
          <AccordionDetails>

            <Table>
              <TableHead>
                <TableRow>
                  <TableCell style={{ width: '30%' }}>Disciplina</TableCell>
                  <TableCell align="right">Situação</TableCell>
                  <TableCell align="right">Nota Final</TableCell>
                  <TableCell align="right">Creditos</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {semestreDisciplinas[key].map((semestreDisciplina) => (
                  <TableRow key={semestreDisciplina?.id}>
                    <TableCell component="th" scope="row">
                      {semestreDisciplina?.disciplina?.nome}
                    </TableCell>
                    <TableCell align="right">{inscricoesInfoById(semestreDisciplina?.disciplina?.id)?.completa} </TableCell>
                    <TableCell align="right">{inscricoesInfoById(semestreDisciplina?.disciplina?.id)?.nota} </TableCell>
                    <TableCell align="right">{semestreDisciplina?.disciplina?.creditos}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>

          </AccordionDetails>
        </Accordion>
        </div>)
    }

    return retval
  }

  return (
    <Paper style={{ minHeight: '80vh', marginLeft: '4vw' }}>
      <div style={{
        padding: '10px',
        textAlign: 'left',
        borderBottom: '1px solid rgb(224, 224, 224)',
        backgroundColor: '#2196f3',
        color: 'white'
      }}> Histórico do Aluno
      </div>

      <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
        <div>
          {disciplines()}
        </div>
      </div>
    </Paper>
  )
}

export default InscricaoTodosPage
