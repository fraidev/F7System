import React, { useEffect, useState } from 'react'
import clsx from 'clsx'
import { makeStyles } from '@material-ui/core/styles'
import CssBaseline from '@material-ui/core/CssBaseline'
import Drawer from '@material-ui/core/Drawer'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import List from '@material-ui/core/List'
import Typography from '@material-ui/core/Typography'
import Divider from '@material-ui/core/Divider'
import IconButton from '@material-ui/core/IconButton'
import Container from '@material-ui/core/Container'
import MenuIcon from '@material-ui/icons/Menu'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import { Redirect, Route, Switch, useHistory } from 'react-router-dom'
import ListItem from '@material-ui/core/ListItem'
import ListItemIcon from '@material-ui/core/ListItemIcon'
import ListItemText from '@material-ui/core/ListItemText'
import PeopleIcon from '@material-ui/icons/People'
import EstudantePage from './EstudantePage'
import { ExitToApp } from '@material-ui/icons'
import MatriculaPage from './MatriculaPage'
import HistoricoEstudante from './HistoricoEstudante'
import InscricaoAtualPage from './InscricaoAtualPage'
import PersonIcon from '@material-ui/icons/Person'
import TurmaPage from './TurmaPage'
import AppsIcon from '@material-ui/icons/Apps'
import { getEu } from '../services/pessoa-service'
import { PessoaUsuario } from '../model/estudante-model'
import AssignmentIcon from '@material-ui/icons/Assignment'
import GradeIlustradaPage from './GradeIlustradaPage'

const drawerWidth = 240

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    display: 'flex'
  },
  toolbar: {
    paddingRight: 24 // keep right padding when drawer closed
  },
  toolbarIcon: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: '0 8px',
    ...theme.mixins.toolbar
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen
    })
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen
    })
  },
  menuButton: {
    marginRight: 36
  },
  menuButtonHidden: {
    display: 'none'
  },
  title: {
    flexGrow: 1
  },
  drawerPaper: {
    whiteSpace: 'nowrap',
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen
    })
  },
  drawerPaperClose: {
    overflowX: 'hidden',
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen
    }),
    width: theme.spacing(7),
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing(9)
    }
  },
  appBarSpacer: theme.mixins.toolbar,
  content: {
    flexGrow: 1,
    height: '100vh',
    overflow: 'auto',
    width: '75vw'
  },
  container: {
    paddingTop: theme.spacing(4),
    paddingBottom: theme.spacing(4)
  },
  paper: {
    padding: theme.spacing(2),
    display: 'flex',
    overflow: 'auto',
    flexDirection: 'column'
  },
  fixedSize: {
    height: 'calc(100% - 186px)',
    width: '80vw'
  }
}))

const Layout: React.FC = () => {
  const history = useHistory()
  const [pessoa, setPessoa] = useState<PessoaUsuario>()
  const classes = useStyles()
  const [open, setOpen] = React.useState(false)
  const handleDrawerOpen = () => {
    setOpen(true)
  }
  const handleDrawerClose = () => {
    setOpen(false)
  }
  const onExit = () => {
    localStorage.setItem('user', '')
    window.location.pathname = '/'
  }

  useEffect(() => {
    const user: { username: string } = JSON.parse(localStorage.getItem('user'))
    getEu(user?.username).then(res => {
      setPessoa(res.data)
    })
  }, [])

  const homepage = () => {
    switch (pessoa?.perfil) {
      case 'Administrator':
        return '/estudante'
      case 'Estudante':
        return `/matricula/${pessoa?.id}/inscricaotodos/${pessoa?.matriculas?.find(x => x.ativo)?.id}`
    }
  }
  return (

    <div className={classes.root}>
      <CssBaseline/>
      <AppBar position="absolute" className={clsx(classes.appBar, open && classes.appBarShift)}>
        <Toolbar className={classes.toolbar}>
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            className={clsx(classes.menuButton, open && classes.menuButtonHidden)}
          >
            <MenuIcon/>
          </IconButton>
          <Typography component="h1" variant="h6" color="inherit" noWrap className={classes.title}>
            System F7
          </Typography>
          <IconButton color="inherit" onClick={onExit}>
            <ExitToApp/>
          </IconButton>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        classes={{
          paper: clsx(classes.drawerPaper, !open && classes.drawerPaperClose)
        }}
        open={open}
      >
        <div className={classes.toolbarIcon}>
          <IconButton onClick={handleDrawerClose}>
            <ChevronLeftIcon/>
          </IconButton>
        </div>
        <Divider/>
        <List>{mainListItems(history, pessoa)}</List>
        <Divider/>
      </Drawer>
      <main className={classes.content}>
        <div className={classes.appBarSpacer}/>
        <Container maxWidth="xl" className={classes.container}>

          <Switch>
            <Route exact path="/">
              <Redirect to={homepage()}/>
            </Route>
            <Route path='/estudante'>
              <EstudantePage/>
            </Route>
            <Route path='/matricula/:estudanteId/inscricaotodos/:matriculaId'>
              <HistoricoEstudante/>
            </Route>
            <Route path='/matricula/:estudanteId/inscricaoatual/:matriculaId'>
              <InscricaoAtualPage/>
            </Route>
            <Route path='/matricula/:estudanteId/gradeilustrada/:matriculaId'>
              <GradeIlustradaPage/>
            </Route>
            <Route path='/matricula/:estudanteId'>
              <MatriculaPage/>
            </Route>

            <Route path='/turma/'>
              <TurmaPage/>
            </Route>

          </Switch>
        </Container>
      </main>
    </div>
  )
}

export const mainListItems = (history: any, pessoa: PessoaUsuario) => {
  const menuEstudante = () => {
    if (pessoa?.perfil === 'Estudante') {
      return (
        <div>
          <ListItem button
            onClick={() => history.push(`/matricula/${pessoa?.id}/inscricaotodos/${pessoa?.matriculas?.find(x => x.ativo)?.id}`)}>
            <ListItemIcon>
              <AppsIcon/>
            </ListItemIcon>
            <ListItemText primary="Grade Curricular"/>
          </ListItem>
          <Divider/>

          <ListItem button
            onClick={() => history.push(`/matricula/${pessoa?.id}/inscricaoatual/${pessoa?.matriculas?.find(x => x.ativo)?.id}`)}>
            <ListItemIcon>
              <AssignmentIcon/>
            </ListItemIcon>
            <ListItemText primary="Inscricão"/>
          </ListItem>
        </div>
      )
    }
  }

  const menuAdmin = () => {
    if (pessoa?.perfil === 'Administrator') {
      return (
        <div>
          <ListItem button onClick={() => history.push('/estudante')}>
            <ListItemIcon>
              <PersonIcon/>
            </ListItemIcon>
            <ListItemText primary="Estudantes"/>
          </ListItem>
          <Divider/>

          <ListItem button onClick={() => history.push('/turma')}>
            <ListItemIcon>
              <PeopleIcon/>
            </ListItemIcon>
            <ListItemText primary="Turmas"/>
          </ListItem>
        </div>
      )
    }
  }

  return (
    <div>

      {menuEstudante()}

      {menuAdmin()}

    </div>
  )
}

export default Layout
