import React, { useEffect, useState } from 'react'
import { Grid } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import { Turma } from '../model/estudante-model'

const useStyles = makeStyles(() => ({
  title: {
    fontSize: '15px'
  },
  horario: {
    fontSize: '12px'
  },
  grid: {
    border: '1px solid #a7a7a7'
  }
}))

type GradeProps = {
  selectedTurmas: Turma[]
}

const initialState = [
  '',
  '',
  '',
  '',
  '',
  '',
  '',
  '',
  '',
  ''
]

const genarateGradeType = (turma: Turma, state: string[]) => {
  const disciplinaNome = turma.disciplina.nome
  const sala = turma.sala
  const professor = turma?.professor?.nome
  const horariosIds = turma.horarios.map(x => x.id)

  const newState = [...state]

  horariosIds.forEach(horariosId => {
    newState[horariosId - 1] = disciplinaNome + ' - Sala: ' + sala + ' - Professor: ' + professor
  })

  return newState
}

const Grade: React.FC<GradeProps> = (props) => {
  const classes = useStyles()
  const [state, setState] = useState(initialState)

  useEffect(() => {
    setState([])

    props.selectedTurmas.forEach(turma => {
      const newState = genarateGradeType(turma, state)
      setState(newState)
    })
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props])

  return (
    <div style={{ flex: '1' }}>
      <Grid container spacing={3}>
        <Grid className={classes.grid} item xs={2}>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>Segunda-Feira</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>Terça-feira</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>Quarta-feira</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>Quinta-feira</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>Sexta-feira</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>18:30 -- 20:30</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[0]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[2]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[4]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[6]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[8]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.title}>20:30 -- 22:30</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[1]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[3]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[5]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[7]}</div>
        </Grid>
        <Grid className={classes.grid} item xs={2}>
          <div className={classes.horario}>{state[9]}</div>
        </Grid>
      </Grid>
    </div>)
}

export default Grade
