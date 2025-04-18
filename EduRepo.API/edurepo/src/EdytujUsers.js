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
    const [error, setError] = useState('');  // Stan do przechowywania komunikatu o błędzie

    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth',
        });
        axios
            .get(`/api/auth/users`)
            .then((response) => {
                setUser(response.data);
            })
            .catch((error) => {
                console.error('Błąd podczas pobierania danych roweru:', error);
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
            alert('Rozmiar jest wymagany');
            return;
        }


        const updatedUser = {
            ...user,
            userId: parseInt(id),
        };

        // Pobierz token z localStorage (lub z innego źródła)
        const token = localStorage.getItem('authToken'); // Zmienna authToken może mieć inną nazwę, zależnie od tego, jak go przechowujesz

        axios
            .put(`https://localhost:7032/api/Auth/users/${id}`, updatedUser, {
                headers: {
                    'Authorization': `Bearer ${token}`, // Dodaj token w nagłówkach
                },
            })
            .then(() => {
                navigate(`/details/${id}`);
            })
            .catch((error) => {
                console.error('Błąd podczas aktualizacji roweru:', error);
                if (error.response) {
                    setError(error.response.data); // Przypisz komunikat błędu do stanu
                } else {
                    setError('Nie udało się zaktualizować roweru. Sprawdź czy prawidłowo wpisałeś wszystkie parametry');
                }
                setTimeout(() => {
                    setError('');
                }, 3000);
            });
    };

    return (
        <div className="bike-form">


            {/* Jeśli wystąpił błąd, wyświetl komunikat */}
            {error && <div className="error-message">{error}</div>}

            <form onSubmit={handleSubmit}>

                <div className="form-group">
                    <p className="h2">Edytuj</p>
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
                        name="isElectric"
                        checked={user.isStudent}
                        onChange={handleChange}
                    />
                </div>
                <div className="form-group">
                    <label>Czy Nauczyciel:</label>
                    <input
                        type="checkbox"
                        name="isElectric"
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