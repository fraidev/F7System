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
            sourcePosition: Position.Left,
            targetPosition: Position.Right,
            draggable: false,
            data: {
              label:
                <div style={{ width: '100%', height: '55px', display: 'flex', flexDirection: 'column', placeContent: 'center' }}>
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
              target: prerequisiteId
            })
          })
        })
        edgesAndNodes.push(...edgesColumn)
        edgesAndNodes.push(...nodesColumn)
      }

      setElements([...elements, ...edgesAndNodes])
    })
  }, [matriculaId])

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
