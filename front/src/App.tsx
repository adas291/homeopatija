import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './style.css'
import Navbar from './components/Navbar'
import { Route, Routes } from 'react-router-dom'
import DrugContainer from './components/drugs/drug-container'
import DrugView from './components/drugs/drug-view'
import BurejaView from './components/bureja/bureja'
import IllnessView from './components/illness/Ilness-view'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Navbar/>
      <Routes>
        <Route path="/vaistai" element={<DrugContainer/>}> </Route>
        <Route path="/vaistai/1" element={<DrugView title='Aspirinas' composition={['Miltai', 'Kiaušinis', 'Druska', 'Kepimo milteliai']} price={5.99}/>}> </Route>
        <Route path="/vaistai/2" element={<DrugView title='Ibuprofen' composition={['Kava', 'Kiaušinis', 'Pipirai', 'Soda']} price={7.99}/>}> </Route>
        <Route path="/bureja" element={<BurejaView/>}> </Route>
        <Route path="/ligos" element={<IllnessView/>}> </Route>
      </Routes> 
    </>
  )
}

export default App
