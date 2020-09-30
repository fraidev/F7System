import React, {useEffect, useState} from 'react'
import Paper from '@material-ui/core/Paper'
import {TextField, Button, Table, TableHead, TableRow, TableCell, TableBody} from '@material-ui/core'
import axios from 'axios'
import {authHeader} from '../services/auth-header'
import EditIcon from '@material-ui/icons/Edit'
import DeleteIcon from '@material-ui/icons/Delete'
import IconButton from '@material-ui/core/IconButton'
import AddIcon from '@material-ui/icons/Add'
import {makeStyles} from '@material-ui/core/styles'
import DateFnsUtils from '@date-io/date-fns';
import {KeyboardDatePicker, MuiPickersUtilsProvider} from '@material-ui/pickers';
import {useSnackbar} from "notistack";
import {format, parseISO} from "date-fns";
import {cpfMask} from "../services/cpf-mask";

export interface Student {
  userPersonId: string,
  username: string,
  password: string,
  name: string,
  socialSecurityNumber: string,
  birth: Date
}

const useStyles = makeStyles(() => ({
  fields: {
    margin: '10px'
  },
  cancelButton: {
    color: "white"
  }
}))

const Student: React.FC = () => {
  const { enqueueSnackbar } = useSnackbar();
  const [students, setStudents] = useState([])
  const [editableStudent, setEditableStudent] = useState<Partial<Student>>()
  const [mode, setMode] = useState<'edit' | 'add' | 'none'>('none')
  const classes = useStyles()

  const initialState: Student = {
    userPersonId: '',
    username: '',
    password: '',
    name: '',
    socialSecurityNumber: '',
    birth: parseISO('2018-04-01')
  }

  const config = {
    headers: authHeader()
  }

  useEffect(() => {

    if (config.headers) {
      axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
        setStudents(res.data)
      })
    }

  }, [])

  const onSave = () => {
    if (mode === 'add') {
      axios.post('https://localhost:5001' + '/Student/CreateStudent', {
        username: editableStudent?.username,
        password: editableStudent?.password,
        name: editableStudent?.name,
        socialSecurityNumber: editableStudent?.socialSecurityNumber,
        birth: editableStudent?.birth,
      }, config)
        .then(() => {
          axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
            setStudents(res.data)
            setMode('none')
            enqueueSnackbar('Estudante criado com sucesso.', {variant: 'success'});
          })
        })
    }
    if (mode === 'edit') {
      axios.post('https://localhost:5001' + '/Student/ChangeStudent', {
        id: editableStudent?.userPersonId,
        username: editableStudent?.username,
        name: editableStudent?.name,
        socialSecurityNumber: editableStudent?.socialSecurityNumber,
        birth: editableStudent?.birth,
      }, config)
        .then(() => {
          axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
            setStudents(res.data)
            setMode('none')
            enqueueSnackbar('Estudante alterado com sucesso.', {variant: 'success'});
          })
        })
    }
  }

  const openAdd = () => {
    setEditableStudent(initialState)
    setMode('add')
  }
  const openEdit = (student: any) => {
    setEditableStudent(student)
    setMode('edit')
    console.log(student)
  }
  const onDelete = (student: any) => {
    axios.post('https://localhost:5001' + '/Student/DeleteStudent', {id: student.userPersonId}, config)
      .then(() => {
        axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
          setStudents(res.data)
          enqueueSnackbar('Estudante excluido com sucesso.', {variant: 'success'});
        })
      })
  }

  const onCancel = () => {
    setMode('none')
  }

  return (
    <Paper style={{width: '80vw', marginLeft: '4vw'}}>

      {mode !== 'none'
        ? <div>
          <form noValidate autoComplete="off">

            <TextField className={classes.fields} id="standard-basic" label="Usuario" value={editableStudent?.username}
                       onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                         ...editableStudent,
                         username: e.target?.value
                       })}/>

            {mode === 'add' ?
              <TextField className={classes.fields} id="standard-basic" label="Senha" value={editableStudent?.password}
                         onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                           ...editableStudent,
                           password: e.target?.value
                         })}/> : null}

            <TextField className={classes.fields} id="standard-basic" label="Nome" value={editableStudent?.name}
                       onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                         ...editableStudent,
                         name: e.target?.value
                       })}/>

            <TextField className={classes.fields} id="standard-basic" label="CPF"
                       value={editableStudent?.socialSecurityNumber}
                       onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                         ...editableStudent,
                         socialSecurityNumber: cpfMask(e.target?.value)
                       })}/>

            <MuiPickersUtilsProvider utils={DateFnsUtils}>
              <KeyboardDatePicker
                className={classes.fields}
                disableToolbar
                variant="inline"
                format="dd/MM/yyyy"
                margin="normal"
                id="date-picker-inline"
                label="Birthday"
                value={editableStudent?.birth}
                onChange={(date) => setEditableStudent({
                  ...editableStudent,
                  birth: date as Date
                })}
                KeyboardButtonProps={{
                  'aria-label': 'change date',
                }}
              />
            </MuiPickersUtilsProvider>

          </form>
          <Button className={classes.cancelButton} variant="contained" color="secondary" onClick={onCancel}>
            Cancel
          </Button>
          <Button variant="contained" color="primary" onClick={onSave}>
            Salvar
          </Button>
        </div>
        : null}

      <Table aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>
            <TableCell align="right">Usuario</TableCell>
            <TableCell align="right">CPF</TableCell>
            <TableCell align="right">Data de Nascimento</TableCell>
            <TableCell align="right">
              <div>
                <IconButton style={{backgroundColor: '#2196f3'}} onClick={openAdd} aria-label="add">
                  <AddIcon style={{color: 'white'}} fontSize="large"/>
                </IconButton>
              </div>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {students.map((student: any) => (
            <TableRow key={student?.name}>
              <TableCell component="th" scope="row">
                {student?.name}
              </TableCell>
              <TableCell align="right">{student?.username}</TableCell>
              <TableCell align="right">{student?.socialSecurityNumber}</TableCell>
              <TableCell align="right">{format(parseISO(student?.birth), 'dd/MM/yyyy')}</TableCell>
              <TableCell align="right">

                <IconButton color="inherit" onClick={() => openEdit(student)}>
                  <EditIcon/>
                </IconButton>
                <IconButton color="inherit" onClick={() => onDelete(student)}>
                  <DeleteIcon/>
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

    </Paper>
  )
}

export default Student
