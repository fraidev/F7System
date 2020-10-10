import axios from 'axios'

export const getCursos = () => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + '/Curso/')
}
