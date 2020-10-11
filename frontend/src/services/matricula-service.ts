import axios from 'axios'

export const getMatriculaById = (id: string) => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + `/Matricula/${id}`)
}
