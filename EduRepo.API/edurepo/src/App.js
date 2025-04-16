import React from 'react';
import { Outlet, Navigate } from 'react-router-dom';

import NavBar from './Navbar';

function App() {
    const token = localStorage.getItem('token'); 

    return (
        <>
            <Outlet />
        </>
    );
}

export default App;
