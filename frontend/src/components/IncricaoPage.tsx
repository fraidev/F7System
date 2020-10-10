import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'
import { useParams } from 'react-router-dom'
import { getEstudanteById } from '../services/pessoa-service'
import { getCursos } from '../services/cursos-service'
import { EstudanteModel } from '../model/estudante-model'
import { getGradesByCursoId } from '../services/grades-service'

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
  const [estudante, setEstudante] = useState<EstudanteModel>()
  const { estudanteId, matriculaId } = useParams()

  useEffect(() => {
    getEstudanteById(estudanteId).then((res: any) => {
      setEstudante(res.data)
    })
  }, [estudanteId])

  return (
    <Paper style={{ height: '80vh', marginLeft: '4vw', padding: '20px' }}>
      {estudante?.nome}

      <div>Disciplinas</div>

    </Paper>
  )
}

export default InscricaoPage
