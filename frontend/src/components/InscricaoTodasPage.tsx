import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { useParams } from 'react-router-dom'
import { getMatriculaById } from '../services/matricula-service'
import { Matricula } from '../model/estudante-model'

const InscricaoTodosPage: React.FC = () => {
  const [matricula, setMatricula] = useState<Matricula>()
  const { matriculaId } = useParams()

  useEffect(() => {
    getMatriculaById(matriculaId).then((res: any) => {
      setMatricula(res.data)
      console.log(res.data.inscricoes.flatMap(x => x.turma))
    })
  }, [matriculaId])

  return (
    <Paper style={{ minHeight: '80vh', marginLeft: '4vw' }}>
      <div style={{
        padding: '10px',
        textAlign: 'left',
        borderBottom: '1px solid rgb(224, 224, 224)',
        backgroundColor: '#2196F3',
        color: 'white'
      }}> Todas Inscrições</div>

      <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
        <div>

          <ol className="planetas-list">
            {matricula?.inscricoes?.flatMap(x => (
              <li key={x?.turma?.id}
                style={{
                  fontSize: '16px',
                  textAlign: 'start'
                }}>  {'Disciplina : ' + x?.turma?.disciplina?.nome + ' - Sala: ' + x?.turma?.sala + ' - Professor: ' + x?.turma?.professor?.nome +
              x?.turma?.horarios.reduce((acc, cur, idx) => acc + (idx === 0 ? ' ' : ' - ') +
                cur.diaDaSemana + ' ' + cur.start + ' - ' + cur.end, ' - Horarios: ')}
              </li>
            ))}
          </ol>
        </div>
      </div>
    </Paper>
  )
}

export default InscricaoTodosPage
