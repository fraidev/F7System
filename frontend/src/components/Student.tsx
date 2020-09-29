import React, {useEffect, useState} from 'react'
import Paper from '@material-ui/core/Paper'
import {TextField, Button, Table, TableHead, TableRow, TableCell, TableBody} from '@material-ui/core'
import axios from 'axios'
import {authHeader} from '../services/auth-header'
import EditIcon from '@material-ui/icons/Edit'
import DeleteIcon from '@material-ui/icons/Delete'
import IconButton from '@material-ui/core/IconButton'
import AddIcon from '@material-ui/icons/Add'

export interface Student {
  userPersonId: string,
  username: string,
  password: string,
  name: string
}

const Student: React.FC = () => {
  const [students, setStudents] = useState([])
  const [editableStudent, setEditableStudent] = useState<Partial<Student>>()
  const [mode, setMode] = useState<'edit' | 'add' | 'none'>('none')
  // const [username, setUsername] = useState('')
  // const [password, setPassword] = useState('')
  // const [name, setName] = useState('')

  const config = {
    headers: authHeader()
  }

  useEffect(() => {
    axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
      setStudents(res.data)
    })
  }, [])

  const onSave = () => {
    if (mode === 'add') {
      axios.post('https://localhost:5001' + '/Student/CreateStudent', {
        username: editableStudent?.username,
        password: editableStudent?.password,
        name: editableStudent?.name
      }, config)
        .then(() => {
          axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
            setStudents(res.data)
            setMode('none')
          })
        })
    }
    if (mode === 'edit') {
      axios.post('https://localhost:5001' + '/Student/ChangeStudent', {
        id: editableStudent?.userPersonId,
        username: editableStudent?.username,
        name: editableStudent?.name
      }, config)
        .then(() => {
          axios.get('https://localhost:5001' + '/Student/', config).then((res: any) => {
            setStudents(res.data)
            setMode('none')
          })
        })
    }
  }

  const openAdd = () => {
    setEditableStudent({})
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
            <TextField id="standard-basic" label="Usuario" value={editableStudent?.username}
                       onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                         ...editableStudent,
                         username: e.target?.value
                       })}/>

            {mode === 'add' ? <TextField id="standard-basic" label="Senha" value={editableStudent?.password}
                                         onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                                           ...editableStudent,
                                           password: e.target?.value
                                         })}/> : null}

            <TextField id="standard-basic" label="Nome" value={editableStudent?.name}
                       onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                         ...editableStudent,
                         name: e.target?.value
                       })}/>
          </form>
          <Button variant="contained" color="secondary" onClick={onCancel}>
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
