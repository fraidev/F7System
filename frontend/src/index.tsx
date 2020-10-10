import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import * as serviceWorker from './serviceWorker'
import App from './App'
import axios from 'axios'
import { authHeader } from './services/auth-header'

axios.interceptors.request.use(function (config) {
  config.headers = authHeader()
  return config
})

ReactDOM.render(
  <App/>,
  document.getElementById('root')
)

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister()
