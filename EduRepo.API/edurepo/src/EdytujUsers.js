import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useParams, Link } from 'react-router-dom';

function EditUser() {
    const [user, setUser] = useState({
        id: '',
        userName: '',
        email: '',
        isStudent: false,
        isTeacher: false,
    });
    const [error, setError] = useState('');
    const { id } = useParams(); 
    const navigate = useNavigate();

    
    const fetchUser = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(`https://localhost:7157/api/auth/users/${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUser(response.data);
        } catch (err) {
            console.error('Błąd podczas pobierania użytkownika:', err);
            setError('Błąd podczas pobierania danych.');
        }
    };

    useEffect(() => {
        fetchUser();
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
    }, [id]); 

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setUser({
            ...user,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!user.userName) {
            alert('Nazwa jest wymagana');
            return;
        }
        if (!user.email) {
            alert('Email jest wymagany');
            return;
        }

        const updatedUser = {
            ...user,
            userId: parseInt(id),
        };

        const token = localStorage.getItem('authToken');

        axios
            .put(`https://localhost:7157/api/Auth/user/${id}`, updatedUser, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            })
            .then(() => {
                navigate(`/kursy`);
            })
            .catch((error) => {
                console.error('Błąd podczas aktualizacji użytkownika:', error);
                if (error.response) {
                    setError(error.response.data); 
                } else {
                    setError('Nie udało się zaktualizować użytkownika. Sprawdź czy prawidłowo wpisałeś wszystkie parametry');
                }
                setTimeout(() => {
                    setError('');
                }, 3000);
            });
    };

    return (
        <div className="user-form">
            {error && <div className="error-message">{error}</div>}

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <p className="h2">Edytuj użytkownika</p>
                    <label>Nazwa:</label>
                    <input type="text" name="userName" value={user.userName} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label>Email:</label>
                    <input type="text" name="email" value={user.email} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label>Czy Student:</label>
                    <input
                        type="checkbox"
                        name="isStudent"
                        checked={user.isStudent}
                        onChange={handleChange}
                    />
                </div>

                <div className="form-group">
                    <label>Czy Nauczyciel:</label>
                    <input
                        type="checkbox"
                        name="isTeacher"
                        checked={user.isTeacher}
                        onChange={handleChange}
                    />
                </div>

                <button type="submit" className="btn btn-primary">
                    Zapisz zmiany
                </button>
                <Link to="/kursy" className="btn btn-secondary">
                    Powrót
                </Link>
            </form>
        </div>
    );
}

export default EditUser;
