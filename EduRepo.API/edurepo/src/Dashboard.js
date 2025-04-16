import React from 'react';
import { Link } from 'react-router-dom';
import './Styles/Dashboard.css';

const Dashboard: React.FC = () => {
    const username = localStorage.getItem('displayName');
    const role = localStorage.getItem('role');

    return (
        <>
            <div className="title1">
                <div className="username1">Witaj, {username}!</div>
            <h1>EduRepo </h1>
                <h2>Twoje osobiste repozytorium zadan!</h2>
            </div>
            <div className="dashboard-container">

            <div className="tile-container">
                <div className="tile">
                    <Link to="/kursy" className="tile-link">Wyszukaj Kursy</Link>
                </div>

                <div className="tile">
                    <Link to="/profile" className="tile-link2">Zarzadzaj Profilem</Link>
                </div>
            </div>
        </div></>
    );
};

export default Dashboard;