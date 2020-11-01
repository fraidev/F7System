import axios from 'axios'

export const getMatriculaById = (id: string) => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + `/Matricula/${id}`)
}

export const getMatriculaAtualById = (id: string) => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + `/Matricula/Atual/${id}`)
}

export const ativarMatricula = (matriculaId: string) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Matricula/Ativar/', { matriculaId: matriculaId })
}
