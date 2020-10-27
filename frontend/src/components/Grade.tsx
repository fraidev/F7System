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
    height: '80px',
    border: '1px solid #a7a7a7',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center'
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

const genarateGradeType = (turmas: Turma[]) => {
  const state = [...initialState]

  turmas.forEach(turma => {
    const disciplinaNome = turma.disciplina.nome
    const sala = turma.sala
    const professor = turma?.professor?.nome
    const horariosIds = turma.horarios.map(x => x.id)

    horariosIds.forEach(horariosId => {
      state[horariosId - 1] = disciplinaNome + ' - Sala: ' + sala + ' - Professor: ' + professor
    })
  })

  return state
}

const Grade: React.FC<GradeProps> = (props) => {
  const classes = useStyles()
  const [state, setState] = useState(initialState)

  useEffect(() => {
    setState([...initialState])
    const newState = genarateGradeType(props?.selectedTurmas ?? [])
    setState(newState)
  }, [props])

  return (
    <div style={{ flex: '1' }}>
      <Grid container >
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
