
import './App.css';

import { Route, Routes } from 'react-router-dom';

import Login from './screens/Login';
import CommunitiesMap from './screens/CommunitiesMap';
import Register from './screens/Register';
import NewCommunity from './screens/NewCommunity';
import Home from './screens/tabs/Home';



import CommunityContextProvider from './contexts/CommunityContextProvider';
import NewRequest from './screens/tabs/NewRequest';
import RequestDisplay from './components/RequestDisplay';


function App() {

  return (
    <CommunityContextProvider>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/CommunitiesMap" element={<CommunitiesMap />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/NewCommunity" element={<NewCommunity />} />
        
        <Route path="/NewRequest" element={<NewRequest />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/RequestDisplay" element={<RequestDisplay />} />
      </Routes>
    </CommunityContextProvider>
  )
}

export default App
