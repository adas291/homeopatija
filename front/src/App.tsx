import './index.css'
import Navbar from './components/Navbar'
import { Route, Routes } from 'react-router-dom'
import DrugView from './components/drugs/drug-view'
import BurejaView from './components/bureja/bureja-page'
import IllnessView from './components/illness/Ilness-view'
import DrugsPage from './components/drugs/drugs-page'

function App() {

  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/vaistai/" element={<DrugsPage />}> </Route>
        <Route path="/vaistai/:id" element={<DrugView />}> </Route>
        <Route path="/bureja" element={<BurejaView />}> </Route>
        <Route path="/ligos" element={<IllnessView />}> </Route>
      </Routes>
    </>
  )
}

export default App
