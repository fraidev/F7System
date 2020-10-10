import axios from 'axios'

export const getGradesByCursoId = (cursoId: string) => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + `/Grade/GradesByCurso/${cursoId}`)
}

export const getGrades = () => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + '/Grade/')
}
