import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { Matricula } from '../model/estudante-model'
import { useParams } from 'react-router-dom'
import { getMatriculaById } from '../services/matricula-service'
import _ from 'lodash'

const GradeIlustradaPage: React.FC = () => {
  const [matricula, setMatricula] = useState<Matricula>()
  const { matriculaId, estudanteId } = useParams()

  useEffect(() => {
    getMatriculaById(matriculaId).then((res: { data: Matricula }) => {
      setMatricula(res.data)
    })
  }, [matriculaId])

  const disciplines = () => {
    const semestreDisciplinas = _.groupBy(matricula?.grade?.semestreDisciplinas, x => x.semestre)

    const inscricoesInfoById = (id) => {
      const inscricao = matricula.inscricoes.find(x => x?.turma?.disciplina.id === id)

      if (inscricao) {
        return {
          completa: inscricao ? (inscricao.completa ? 'Concluido' : 'Cursando') : 'Pendente',
          nota: inscricao && inscricao.notaFinal ? inscricao.notaFinal : '-'
        }
      }
    }

    const retval = []

    for (const key in semestreDisciplinas) {
      retval.push(
        <div key={key}> {semestreDisciplinas[key].map((semestreDisciplina) => (
          <div style={{ border: 'solid', margin: '10px', maxWidth: '180px' }} key={semestreDisciplina?.id}>
            <div>{semestreDisciplina?.disciplina?.nome} </div>
            <div>{semestreDisciplina?.disciplina?.creditos}</div>
          </div>
        ))}
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
      }}> Grade Ilustrada
      </div>

      <div style={{ display: 'flex', height: '100%' }}>
        {disciplines()}
      </div>
    </Paper>
  )
}
export default GradeIlustradaPage
