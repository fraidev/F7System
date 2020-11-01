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
  disciplina: Disciplina;
  semestre?: any;
  horarios: Horario[];
  turmaHorarios: TurmaHorario[];
}

export interface Disciplina {
  id: string;
  nome: string;
  creditos: number;
  prerequisites: Disciplina[];
  turmas: Turma[];
}

export interface SemestreDisciplina {
  id: string;
  semestre: number;
  disciplina: Disciplina;
}

export interface Grade {
  id: string;
  curso?: any;
  cursoId: string;
  ano: number;
  semestreDisciplinas: SemestreDisciplina[];
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
  matriculas: Matricula[];
  turmas: Turma[];
}

export interface Inscricao {
  id: string;
  dataInscricao: Date;
  notaFinal: number;
  turma: Turma;
  completa: boolean;
  matriculaId: string;
}

export interface Matricula {
  ativo: boolean;
  id: string;
  grade: Grade;
  pessoaUsuarioId: string;
  pessoaUsuario: PessoaUsuario;
  inscricoes: Inscricao[];
}
