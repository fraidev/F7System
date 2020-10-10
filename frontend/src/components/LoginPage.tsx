import React, { useState } from 'react'
import axios from 'axios'

import {
  makeStyles,
  TextField,
  Button
} from '@material-ui/core'

const LoginPage: React.FC = () => {
  const classes = useStyles()
  const [state, setState] = useState({
    submitted: false,
    loading: false,
    error: ''
  })
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')

  const handleSubmit = (e: any) => {
    e.preventDefault()
    console.log(process.env.REACT_APP_BACKEND_BASE_URL)
    axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/user/authenticate', { username, password })
      .then(res => {
        console.log(res)
        localStorage.setItem('user', JSON.stringify(res.data))
        window.location.pathname = '/'
      })
  }

  const { submitted, loading, error } = state
  return (
    <div>
      <div>
        Username: admin<br/>
        Password: admin
      </div>
      <h2>Login</h2>
      <form name="form" onSubmit={handleSubmit}>
        <div>
          <TextField
            size="small"
            variant="outlined"
            label="Usuario"
            className={classes.username}
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setUsername(e.target?.value)}
            onKeyDown={(e) => e.key === 'Enter' && handleSubmit(e)}
          ></TextField>

          <br></br>
          <TextField
            className={classes.username}
            size="small"
            type="password"
            variant="outlined"
            label="Senha"
            onChange={(e: React.ChangeEvent<HTMLInputElement>) => setPassword(e.target?.value)}
            onKeyDown={(e) => e.key === 'Enter' && handleSubmit(e)}
          ></TextField>
        </div>

        <div>
          <Button
            type="submit"
            className={classes.loginButton}
            variant="contained" color="secondary">
            Acessar
          </Button>
          {loading &&
          <img
            src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="/>
          }
        </div>
      </form>
    </div>
  )
}

const useStyles = makeStyles({
  username: {
    paddingBottom: '10px'
  },
  loginButton: {
    marginLeft: '5px',
    height: '39px',
    color: 'white',
    fontSize: '15px',
    width: '215px'
  }
})
export default LoginPage
