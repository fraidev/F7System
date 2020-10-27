import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { Button, Menu, MenuItem, Table, TableBody, TableCell, TableHead, TableRow, TextField } from '@material-ui/core'
import EditIcon from '@material-ui/icons/Edit'
import DeleteIcon from '@material-ui/icons/Delete'
import IconButton from '@material-ui/core/IconButton'
import AddIcon from '@material-ui/icons/Add'
import { makeStyles } from '@material-ui/core/styles'
import DateFnsUtils from '@date-io/date-fns'
import { KeyboardDatePicker, MuiPickersUtilsProvider } from '@material-ui/pickers'
import { useSnackbar } from 'notistack'
import { format, parseISO } from 'date-fns'
import { cpfMask } from '../services/cpf-mask'
import { EstudanteModel, initialStateEstudante } from '../model/estudante-model'
import { createPessoa, deletePessoa, getEstudantes, updatePessoa } from '../services/pessoa-service'
import MoreVertIcon from '@material-ui/icons/MoreVert'
import Fade from '@material-ui/core/Fade'
import { useHistory } from 'react-router-dom'

const useStyles = makeStyles(() => ({
  fields: {
    margin: '10px'
  },
  cancelButton: {
    color: 'white'
  }
}))

const EstudantePage: React.FC = () => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null)
  const open = Boolean(anchorEl)
  const history = useHistory()
  const { enqueueSnackbar } = useSnackbar()
  const [students, setStudents] = useState([])
  const [editableStudent, setEditableStudent] = useState<Partial<EstudanteModel>>()
  const [mode, setMode] = useState<'edit' | 'add' | 'none'>('none')
  const classes = useStyles()

  useEffect(() => {
    getEstudantes().then((res: any) => {
      setStudents(res.data)
    })
  }, [])

  const onSave = () => {
    if (mode === 'add') {
      createPessoa(editableStudent).then(() => {
        getEstudantes().then((res: any) => {
          setStudents(res.data)
          setMode('none')
          enqueueSnackbar('Pessoa criado com sucesso.', { variant: 'success' })
        })
      })
    }
    if (mode === 'edit') {
      updatePessoa(editableStudent).then(() => {
        getEstudantes().then((res: any) => {
          setStudents(res.data)
          setMode('none')
          enqueueSnackbar('Pessoa alterado com sucesso.', { variant: 'success' })
        })
      })
    }
  }

  const openAdd = () => {
    setEditableStudent(initialStateEstudante)
    setMode('add')
  }
  const openEdit = (student: any) => {
    setEditableStudent(student)
    setMode('edit')
  }

  const onDelete = (student: any) => {
    deletePessoa(student.id).then(() => {
      getEstudantes().then((res: any) => {
        setStudents(res.data)
        enqueueSnackbar('Pessoa excluido com sucesso.', { variant: 'success' })
      })
    })
  }

  const onCancel = () => {
    setMode('none')
  }
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget)
  }
  const handleClose = (estudante) => {
    if (estudante?.id) {
      history.push(`/matricula/${estudante.id}`)
    }
    setAnchorEl(null)
  }

  return (

    <Paper style={{ minHeight: '80vh', marginLeft: '4vw' }}>
      <div style={{
        padding: '10px',
        textAlign: 'left',
        borderBottom: '1px solid rgb(224, 224, 224)',
        backgroundColor: '#2196F3',
        color: 'white'
      }}>Estudantes</div>

      {mode !== 'none'
        ? <div>
          <form noValidate autoComplete="off">

            <TextField className={classes.fields} id="standard-basic" label="Usuario"
              value={editableStudent?.username}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                ...editableStudent,
                username: e.target?.value
              })}/>

            {mode === 'add'
              ? <TextField className={classes.fields} id="standard-basic" label="Senha"
                value={editableStudent?.password}
                type='password'
                onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                  ...editableStudent,
                  password: e.target?.value
                })}/> : null}

            <TextField className={classes.fields} id="standard-basic" label="Nome" value={editableStudent?.nome}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                ...editableStudent,
                nome: e.target?.value
              })}/>

            <TextField className={classes.fields} id="standard-basic" label="CPF"
              value={editableStudent?.cpf}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setEditableStudent({
                ...editableStudent,
                cpf: cpfMask(e.target?.value)
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
                value={editableStudent?.dataNascimento}
                onChange={(date) => setEditableStudent({
                  ...editableStudent,
                  dataNascimento: date as Date
                })}
                KeyboardButtonProps={{
                  'aria-label': 'change date'
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

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Nome</TableCell>
            <TableCell align="right">Usuario</TableCell>
            <TableCell align="right">CPF</TableCell>
            <TableCell align="right">Data de Nascimento</TableCell>
            <TableCell align="right">
              <div>
                <IconButton style={{ backgroundColor: '#2196f3' }} onClick={openAdd} aria-label="add">
                  <AddIcon style={{ color: 'white' }} fontSize="large"/>
                </IconButton>
              </div>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {students.map((estudante: any) => (
            <TableRow key={estudante?.id}>
              <TableCell component="th" scope="row">
                {estudante?.nome}
              </TableCell>
              <TableCell align="right">{estudante?.username}</TableCell>
              <TableCell align="right">{estudante?.cpf}</TableCell>
              <TableCell align="right">{format(parseISO(estudante?.dataNascimento), 'dd/MM/yyyy')}</TableCell>
              <TableCell align="right">

                <IconButton color="inherit" onClick={() => openEdit(estudante)}>
                  <EditIcon/>
                </IconButton>
                <IconButton color="inherit" onClick={() => onDelete(estudante)}>
                  <DeleteIcon/>
                </IconButton>
                <IconButton color="inherit" onClick={handleClick}>
                  <MoreVertIcon/>
                </IconButton>
                <Menu
                  id="fade-menu"
                  anchorEl={anchorEl}
                  keepMounted
                  open={open}
                  onClose={handleClose}
                  TransitionComponent={Fade}>

                  <MenuItem onClick={() => handleClose(estudante)}>Matriculas</MenuItem>
                </Menu>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

    </Paper>
  )
}

export default EstudantePage
