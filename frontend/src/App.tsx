import React from 'react'
import { makeStyles, createMuiTheme, ThemeProvider } from '@material-ui/core'
import { blue } from '@material-ui/core/colors'
import {
  BrowserRouter as Router,
  Switch,
  Route
} from 'react-router-dom'
import LoginPage from './components/LoginPage'
import { PrivateRoute } from './components/PrivateRoute'
import Layout from './components/Layout'

function App() {
  const classes = useStyles()

  return (
    <div className={classes.App}>
      <ThemeProvider theme={theme}>
        <Router>
          <Switch>
            <Route path="/login" component={LoginPage}/>
            <PrivateRoute path="/" component={Layout}/>
          </Switch>
        </Router>
      </ThemeProvider>
    </div>
  )
}

const theme = createMuiTheme({
  palette: {
    primary: blue,
    secondary: {
      main: '#3fb57f'
    }
  }
})

const useStyles = makeStyles({
  App: {
    textAlign: 'center',
    // backgroundColor: '#282c34',
    minHeight: '80vh',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    fontSize: 'calc(10px + 2vmin)'
    // color: 'white'
  }
})

export default App
