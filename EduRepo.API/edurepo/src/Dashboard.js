import React from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import './Styles/Dashboard.css';

const Dashboard: React.FC = () => {
    const username = localStorage.getItem('displayName');
    const role = localStorage.getItem('role');
    const token = localStorage.getItem('token');
    //const [userToken, setUserToken] = React.useState(localStorage.getItem('token'));

    const navigate = useNavigate();
    return (
        <>
            <div className="title1">
                <div className="username1">Witaj, {username}!</div>

            </div>

            {username ? (
                <button
                    type="button"
                    className="login-register-button2"
                    onClick={(e) => {
                        e.preventDefault();
                        localStorage.removeItem('authToken');
                        localStorage.removeItem('token');
                        localStorage.removeItem('displayName');
                        localStorage.removeItem('role');
                        //setUserToken(null);
                        navigate("/navbar");
                    }}
                >
                    Wyloguj się
                </button>
            ) : (
                <button
                    type="button"
                    className="login-register-button2"
                    onClick={(e) => {
                        e.preventDefault();
                        navigate("/navbar");
                    }}
                >
                    Zaloguj się
                </button>
            )}

            
            <div className="dashboard-container">
                <h1>EduRepo </h1>
                <h2>Twoje osobiste repozytorium zadań!</h2>
                <div className="tile-container">
                    <div className="tile">
                        <Link to="/kursy" className="tile-link">Wyszukaj Kursy</Link>
                    </div>

                    <div className="tile">
                        <Link to="/profile" className="tile-link2">Zarządzaj Profilem</Link>
                    </div>
                    {role === "Student" && (
                        <div className="tile">
                            <Link to="/mojekursy" className="tile-link2">Moje Kursy</Link>
                        </div>
                    )}

                    {role === 'Admin' && (
                        <div className="users">
                            <Link to="/users" className="tile-link2">Użytkownicy</Link>
                        </div>
                    )}
                                

                </div>


            </div>

        </>
    );
};

export default Dashboard;