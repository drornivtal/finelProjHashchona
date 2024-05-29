

// import { BottomNavigation, BottomNavigationAction } from '@mui/material';
// import { Home as HomeIcon, AddCircleOutline as AddCircleOutlineIcon, Person as PersonIcon } from '@mui/icons-material';
// import { useNavigate } from 'react-router-dom';

// export default function NavigationBar() {
//     const navigate = useNavigate();

//     const handleNavigation = (path) => {
//         navigate(path);
//     };

//     return (
//         <BottomNavigation
//             sx={{
//                 backgroundColor: '#9274B2',
//                 opacity:0.8

//             }}
//         >
//             <BottomNavigationAction label="Home" icon={<HomeIcon />} onClick={() => handleNavigation('/Home')} />
//             <BottomNavigationAction label="New Request" icon={<AddCircleOutlineIcon />} onClick={() => handleNavigation('/NewRequest')} />
//             <BottomNavigationAction label="Profile" icon={<PersonIcon />} onClick={() => handleNavigation('/Profile')} />
//         </BottomNavigation>
//     );
// };

import React from 'react';
import { BottomNavigation, BottomNavigationAction } from '@mui/material';
import { Home as HomeIcon, HomeOutlined as HomeOutlinedIcon, AddCircleOutlineOutlined as AddCircleOutlineOutlinedIcon, AddCircleOutlined as AddCircleOutlinedIcon, PersonOutlineOutlined as PersonOutlineOutlinedIcon, Person as PersonIcon } from '@mui/icons-material';
import { useLocation, useNavigate } from 'react-router-dom';

export default function NavigationBar() {
    const location = useLocation();
    const navigate = useNavigate();

    const handleNavigation = (path) => {
        navigate(path);
    };

    return (
        <BottomNavigation showLabels={false} sx={{ backgroundColor: '#bda8dd', borderTop: '1px solid #9274B2' }}>
            <BottomNavigationAction
                // icon={<HomeIcon sx={{ fontSize: '2rem' }} />}
                icon={location.pathname === '/Home' ? <HomeIcon sx={{ fontSize: '1.9rem' }} /> : <HomeOutlinedIcon sx={{ fontSize: '1.9rem' }} />}
                onClick={() => handleNavigation('/Home')}
                selected={location.pathname === '/Home'}
            />
            <BottomNavigationAction
                icon={location.pathname === '/NewRequest' ? <AddCircleOutlinedIcon sx={{ fontSize: '1.9rem' }} /> : <AddCircleOutlineOutlinedIcon sx={{ fontSize: '1.9rem' }} />}
                onClick={() => handleNavigation('/NewRequest')}
                selected={location.pathname === '/NewRequest'}
            />
            <BottomNavigationAction
                icon={location.pathname === '/Profile' ? <PersonIcon sx={{ fontSize: '1.9rem' }} /> : <PersonOutlineOutlinedIcon sx={{ fontSize: '1.9rem' }} />}
                onClick={() => handleNavigation('/Profile')}
                selected={location.pathname === '/Profile'}
            />
        </BottomNavigation>
    );
};


