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

export interface Horario {
  id: number;
  dayOfWeek: string;
  diaDaSemana: string;
  start: string;
  end: string;
}

export interface TurmaHorario {
  horarioId: number;
  horario: Horario;
  turmaId: string;
}

export interface Professor {
  id: string;
  username?: any;
  passwordHash?: any;
  passwordSalt?: any;
  token?: any;
  perfil: string;
  nome: string;
  cpf?: any;
  dataNascimento: Date;
  matriculas: any[];
  turmas: Turma[];
}

export interface Turma {
  id: string;
  sala: string;
  professor: Professor;
  semestre?: any;
  horarios: Horario[];
  turmaHorarios: TurmaHorario[];
  disciplina: Disciplina;
}

export interface Disciplina {
  id: string;
  nome: string;
  creditos: number;
  turmas: Turma[];
}

export interface Grade {
  id: string;
  curso?: any;
  cursoId: string;
  ano: number;
  disciplinas: Disciplina[];
}

export interface PessoaUsuario {
  id: string;
  username: string;
  passwordHash?: any;
  passwordSalt?: any;
  token?: any;
  perfil: string;
  nome: string;
  cpf: string;
  dataNascimento: Date;
  matriculas: any[];
  turmas: any[];
}

export interface Matricula {
  id: string;
  grade: Grade;
  pessoaUsuarioId: string;
  pessoaUsuario: PessoaUsuario;
  inscricoes: any[];
}
