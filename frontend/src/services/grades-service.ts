import axios from 'axios'

export const getGrades = () => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/Estudantes')
}
