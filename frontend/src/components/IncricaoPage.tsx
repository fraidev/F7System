import React, { useEffect } from 'react'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'
import { useParams } from 'react-router-dom'

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
  const { matriculaId } = useParams()

  return (
    <Paper style={{ height: '80vh', marginLeft: '4vw', padding: '20px' }}>
      {matriculaId}
      teste

    </Paper>
  )
}

export default InscricaoPage
