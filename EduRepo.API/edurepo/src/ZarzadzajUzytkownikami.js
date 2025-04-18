import React, { useEffect, useState } from 'react';
import { useNavigate, Link, useParams, NavLink } from 'react-router-dom';
import { Menu, Button, List } from 'semantic-ui-react';
import axios from 'axios';
import './Styles/Users.css';

function ZarzadzajUzytkownikami() {
    const [users, setUsers] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchUsers = async () => {
        try {
            const token = localStorage.getItem('token'); 
            const response = await axios.get('https://localhost:7157/api/auth/users', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUsers(response.data);
        } catch (err) {
            console.error('Błąd podczas pobierania użytkowników:', err);
            setError('Błąd podczas pobierania danych.');
        } finally {
            setIsLoading(false);
        }
    };

 
    const handleEdit = () => {
        console.log('Dodaj zadanie dla kursu (tylko Admin)');
    };
    useEffect(() => {
        fetchUsers();
    }, []);

    if (isLoading) return <p>Ładowanie użytkowników...</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;

    return (
        <div className="user-list">
            <h2>Lista użytkowników</h2>
            {users.map((user) => {
                const currentRole = user.isTeacher ? 'Teacher' : user.isStudent ? 'Student' : 'Admin';

                return (
                    <div key={user.id} className="user-card">
                        <h4>{user.userName}</h4>
                        <p><strong>Email:</strong> {user.email}</p>
                        <p><strong>Aktualna rola:</strong> {currentRole}</p>

 
                        <Link to={`/edytuj/${user.id}`} className="edit-button">
                            Edytuj
                        </Link>
                    </div>
                );
            })}
            <Link to="/dashboard" className="btn btn-secondary">
                Powrót
            </Link>
        </div>
    );
}

export default ZarzadzajUzytkownikami;
