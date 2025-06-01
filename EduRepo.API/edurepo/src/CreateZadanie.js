import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import "./Styles/CreateZadanie.css";



function CreateZadanie() {
    const token = localStorage.getItem('authToken');

    const { id } = useParams(); // kursId
    const navigate = useNavigate();
    const [error, setError] = useState(null);
    const [userId, setUserId] = useState(null);
    const [unique_name, setUserName] = useState(null);
    const [file, setFile] = useState(null);

    const [zadanie, setZadanie] = useState({
        nazwa: "",
        tresc: "",
        terminOddania: "",
        czyObowiazkowe: false
    });

    useEffect(() => {
        fetchUserData();
    }, []);

    const parseJwt = (token) => {
        if (!token) return null;
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split('')
                    .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
                    .join('')
            );
            return JSON.parse(jsonPayload);
        } catch (e) {
            return null;
        }
    };

    const fetchUserData = () => {
        const userData = parseJwt(token);
        if (!userData || !userData.nameid) {
            alert("Nie udało się odczytać UserId z tokena.");
            return;
        }
        setUserId(userData.nameid);
        setUserName(userData.unique_name);
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;

        setZadanie(prev => ({
            ...prev,
            [name]: newValue,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();


        const formData = new FormData();
        formData.append("IdKursu", id);
        formData.append("Nazwa", zadanie.nazwa);
        formData.append("Tresc", zadanie.tresc);
        formData.append("TerminOddania", zadanie.terminOddania);
        formData.append("CzyObowiazkowe", zadanie.czyObowiazkowe);
        formData.append("UserId", userId);
        formData.append("Name", unique_name);

        if (file) {
            formData.append("Zalacznik", file);
        }

        try {
            await axios.post('https://localhost:7157/api/Zadanie', formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                    Authorization: `Bearer ${token}`,
                },
            });
            navigate(`/details/${id}`);
        } catch (error) {
            console.error('Błąd podczas tworzenia zadania:', error);
            alert('Wystąpił błąd podczas tworzenia zadania.');
        }
    };

    return (
        <div className="form-container">
            <h4>Utwórz nowe zadanie</h4>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Nazwa zadania:</label>
                    <input
                        type="text"
                        name="nazwa"
                        value={zadanie.nazwa}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Treść:</label>
                    <textarea
                        name="tresc"
                        value={zadanie.tresc}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Termin oddania:</label>
                    <input
                        type="datetime-local"
                        name="terminOddania"
                        value={zadanie.terminOddania}
                        onChange={handleChange}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Załącz plik pomocniczy:</label>
                    <input
                        type="file"
                        onChange={(e) => setFile(e.target.files[0])}
                    />
                </div>

                <div className="form-group">
                    <label>Czy obowiązkowe:</label>
                    <input
                        type="checkbox"
                        name="czyObowiazkowe"
                        checked={zadanie.czyObowiazkowe}
                        onChange={handleChange}
                    />
                </div>

                <button type="submit" className="btn btn-primary">Utwórz zadanie</button>
                <Link to={`/details/${id}`} className="btn btn-secondary">Anuluj</Link>
            </form>
        </div>
    );
}

export default CreateZadanie;
