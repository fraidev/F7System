import React, { useEffect, useState } from 'react'
import Paper from '@material-ui/core/Paper'
import { Matricula } from '../model/estudante-model'
import { useParams } from 'react-router-dom'
import { getMatriculaById } from '../services/matricula-service'
import _ from 'lodash'
import ReactFlow, { Elements, Position } from 'react-flow-renderer'

const GradeIlustradaPage: React.FC = () => {
  const [elements, setElements] = useState<Elements>([])
  const [matricula, setMatricula] = useState<Matricula>()
  const { matriculaId, estudanteId } = useParams()

  useEffect(() => {
    getMatriculaById(matriculaId).then((res: { data: Matricula }) => {
      setMatricula(res.data)

      const semestreDisciplinas = _.groupBy(res.data?.grade?.semestreDisciplinas, x => x.semestre)

      const edgesAndNodes = []

      for (const key in semestreDisciplinas) {
        const edgesColumn = semestreDisciplinas[key].map((semestreDisciplina, index) => {
          return {
            // id: `horizontal-${key}-${index}`,
            id: semestreDisciplina?.disciplina?.id,
            sourcePosition: Position.Right,
            targetPosition: Position.Left,
            draggable: false,
            data: {
              label: <div>
                <div>{semestreDisciplina?.disciplina?.nome}</div>
                <div>{semestreDisciplina?.disciplina?.creditos}</div>
              </div>
            },
            position: { x: 200 * (parseInt(key) - 1), y: 100 * index }
          }
        })
        const nodesColumn = []

        semestreDisciplinas[key].forEach(semestreDisciplina => {
          const preRequisiteIds = semestreDisciplina.disciplina.prerequisites.map(preRequisite => {
            return preRequisite.id
          })

          preRequisiteIds.forEach(prerequisiteId => {
            nodesColumn.push({
              id: 'horizontal-' + semestreDisciplina?.disciplina.id + '-' + prerequisiteId,
              source: semestreDisciplina?.disciplina.id,
              target: prerequisiteId,
              animated: true
            })
          })
        })
        edgesAndNodes.push(...edgesColumn)
        edgesAndNodes.push(...nodesColumn)
      }

      setElements([...elements, ...edgesAndNodes])
    })
  }, [matriculaId])

  // const elements = [
  //   { id: 'horizontal-1', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: 'Node 1' }, position: { x: 50, y: 5 } },
  //   { id: 'horizontal-2', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 50, y: 105 } },
  //   { id: 'horizontal-3', sourcePosition: Position.Left, targetPosition: Position.Right, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 50, y: 205 } },
  //   { id: 'horizontal-4', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 50, y: 305 } },
  //   { id: 'horizontal-5', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 50, y: 405 } },
  //
  //   { id: 'horizontal-6', sourcePosition: Position.Left, targetPosition: Position.Right, draggable: false, data: { label: 'Node 1' }, position: { x: 250, y: 5 } },
  //   { id: 'horizontal-7', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 250, y: 105 } },
  //   { id: 'horizontal-8', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 250, y: 205 } },
  //   { id: 'horizontal-9', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 250, y: 305 } },
  //   { id: 'horizontal-10', sourcePosition: Position.Right, targetPosition: Position.Left, draggable: false, data: { label: () => <div>Node 2</div> }, position: { x: 250, y: 405 } },
  //   { id: 'horizontal-e6-1', source: 'horizontal-6', target: 'horizontal-3', animated: true }
  // ]

  const BasicFlow = () => <ReactFlow nodesDraggable={false} selectNodesOnDrag={false} draggable={false}
    zoomOnDoubleClick={false} style={{ overflow: 'visible' }} elements={elements}>
  </ReactFlow>

  return (
    <Paper style={{ minHeight: '80vh', marginLeft: '4vw' }}>
      <div style={{
        padding: '10px',
        textAlign: 'left',
        borderBottom: '1px solid rgb(224, 224, 224)',
        backgroundColor: '#2196f3',
        color: 'white'
      }}> Grade Ilustrada
      </div>

      <BasicFlow/>
    </Paper>
  )
}
export default GradeIlustradaPage
