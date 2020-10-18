import { Dialog, DialogTitle, List, ListItem, ListItemText } from '@material-ui/core'
import React from 'react'
import { Disciplina } from '../model/estudante-model'

export interface SimpleDialogProps {
  open: boolean;
  onClose: (value: any | null) => void;
  disciplina: Disciplina
}

const TurmasDialog = (props: SimpleDialogProps) => {
  const { onClose, disciplina, open } = props

  const handleClose = () => {
    onClose(null)
  }

  const handleListItemClick = (value: any) => {
    value.disciplina = disciplina
    onClose(value)
  }

  return (
    <Dialog onClose={handleClose} aria-labelledby="simple-dialog-title" open={open}>
      <DialogTitle id="simple-dialog-title">Selecione uma turma</DialogTitle>
      <List>
        {disciplina?.turmas?.map((turma: any) => (
          <ListItem button onClick={() => handleListItemClick(turma)} key={turma}>
            <ListItemText primary={'Sala: ' + turma?.sala + ' - Professor: ' + turma?.professor?.nome}
              secondary={turma?.horarios.reduce((acc, cur, idx) => acc + (idx === 0 ? ' ' : ' - ') + cur.diaDaSemana + ' ' + cur.start + ' - ' + cur.end, 'Horarios: ')}/>
          </ListItem>
        ))}
      </List>
    </Dialog>
  )
}

export default TurmasDialog
