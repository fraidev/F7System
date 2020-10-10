import { parseISO } from 'date-fns'

export interface EstudanteModel {
    id: string,
    username: string,
    password: string,
    nome: string,
    cpf: string,
    dataNascimento: Date,
    perfil: 'Administrator' | 'Estudante' | 'Professor' | 'Secretario';
    matriculas: []
}

export const initialStateEstudante: EstudanteModel = {
  id: '',
  username: '',
  password: '',
  nome: '',
  cpf: '',
  dataNascimento: parseISO('2018-04-01'),
  perfil: 'Estudante',
  matriculas: []
}
