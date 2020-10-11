import axios from 'axios'

export const getEstudanteById = (id: string) => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + `/Pessoa/Estudante/${id}`)
}

export const getEstudantes = () => {
  return axios.get(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/Estudantes')
}

export const createPessoa = (editableStudent) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/CriarPessoa', {
    username: editableStudent?.username,
    password: editableStudent?.password,
    nome: editableStudent?.nome,
    cpf: editableStudent?.cpf,
    dataNascimento: editableStudent?.dataNascimento,
    perfil: editableStudent?.perfil
  })
}

export const updatePessoa = (editableStudent) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/AlterarPessoa', {
    id: editableStudent?.id,
    username: editableStudent?.username,
    nome: editableStudent?.nome,
    cpf: editableStudent?.cpf,
    dataNascimento: editableStudent?.dataNascimento,
    perfil: editableStudent?.perfil
  })
}

export const deletePessoa = (studentId) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/DeletarPessoa', { id: studentId })
}

export const addMatricula = (cmd) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/AddMatriculaEstudante', cmd)
}

export const addInscricoesMatriculaEstudante = (cmd) => {
  return axios.post(process.env.REACT_APP_BACKEND_BASE_URL + '/Pessoa/AddInscricoesMatriculaEstudante', cmd)
}
