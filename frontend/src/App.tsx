import React from 'react'
import { makeStyles, createMuiTheme, ThemeProvider } from '@material-ui/core'
import { blue } from '@material-ui/core/colors'
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom'
import LoginPage from './components/LoginPage'
import SomePage from './components/SomePage'
import { PrivateRoute } from './components/PrivateRoute'

function App() {
  const classes = useStyles()

  return (
    <div className={classes.App}>
      <ThemeProvider theme={theme}>
        <Router>
          <PrivateRoute exact path="/" component={SomePage} />
          <Route path="/login" component={LoginPage} />
        </Router >
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
    fontSize: 'calc(10px + 2vmin)',
    // color: 'white'
  }
})

export default App
