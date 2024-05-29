
import './App.css';

import { Route, Routes, useLocation } from 'react-router-dom';
import { Container } from '@mui/material';

import CommunityContextProvider from './contexts/CommunityContextProvider';

import Login from './screens/Login';
import CommunitiesMap from './screens/CommunitiesMap';
import Register from './screens/Register';
import NewCommunity from './screens/NewCommunity';
import Profile from './screens/tabs/Profile';
import NewRequest from './screens/tabs/NewRequest';
import Home from './screens/tabs/Home';

import RequestDisplay from './components/RequestDisplay';
import NavigationBar from './components/NavigationBar';


function App() {

  const location = useLocation();
  const showNavigationBar = ['/Home', '/NewRequest', '/Profile'].includes(location.pathname);

  return (
    <CommunityContextProvider>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/CommunitiesMap" element={<CommunitiesMap />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/NewCommunity" element={<NewCommunity />} />
        <Route path="/Profile" element={<Profile />} />
        <Route path="/NewRequest" element={<NewRequest />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/RequestDisplay" element={<RequestDisplay />} />
      </Routes>

      {showNavigationBar && (
        <Container sx={{ position: 'fixed', bottom: 0, left: 0, right: 0 }}>
          <NavigationBar />
        </Container>
      )}

    </CommunityContextProvider>
  )
}

export default App
