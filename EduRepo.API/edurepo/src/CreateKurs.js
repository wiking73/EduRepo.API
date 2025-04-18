import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';

const token = localStorage.getItem('authToken');
const rola = localStorage.getItem('role');
function CreateKurs() {
    const [kurs, setKurs] = useState({
        idKursu: "",
        nazwa: "",
        opisKursu: "",
        rokAkademicki: "",
        klasa: "",
        wlascicielUserId: "", 
        wlascicielUserName: "",

    });

    // Stan na nazwisko u¿ytkownika i UserId
    const [unique_name, setUserName] = useState(null);
    const [userId, setUserId] = useState(null);

    const navigate = useNavigate();
    console.log(rola);
    // Funkcja do parsowania tokena
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

    // Pobierz dane u¿ytkownika
    const fetchUserData = () => {
        const userData = parseJwt(token);
        if (!userData || !userData.nameid) {
            alert("Nie uda³o siê odczytaæ UserId z tokena.");
            return;
        }
        setUserId(userData.nameid);
        setUserName(userData.unique_name); 
    };

    // Obs³uga zmiany wartoœci w formularzu
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        let newValue = value;
        if (type === 'checkbox') {
            newValue = checked;
        } else if (type === 'number') {
            newValue = value === '' ? '' : parseFloat(value);
        }

        setKurs({
            ...kurs,
            [name]: newValue,
        });
    };

    // Obs³uga przesy³ania formularza
    const handleSubmit = (e) => {
        e.preventDefault();

        const token = localStorage.getItem('authToken');
        if (!userId) {
            alert("Nie uda³o siê odczytaæ UserId.");
            return;
        }

        const kursToSend = {
            nazwa: kurs.nazwa,
            opisKursu: kurs.opisKursu,
            rokAkademicki: kurs.rokAkademicki,
            klasa: kurs.klasa,
            userId: userId, 
            name: unique_name,
        };

        axios.post('https://localhost:7157/api/Kurs', kursToSend, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
            
        })
            .then(() => navigate('/dashboard'))
            .catch((error) => {
                console.error('B³¹d podczas tworzenia kursu:', error);
                alert('Wyst¹pi³ b³¹d podczas tworzenia kursu.');
            });
    };

    useEffect(() => {
        fetchUserData(); // Pobierz dane u¿ytkownika po za³adowaniu komponentu
    }, []);

    return (
        
        
        <div className="bike-form">
            <h6>Dodaj nowy kurs</h6>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Nazwa:</label>
                    <input
                        type="text"
                        name="nazwa"
                        value={kurs.nazwa}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Opis:</label>
                    <input
                        type="text"
                        name="opisKursu"
                        value={kurs.opisKursu}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Rok akademicki:</label>
                    <input
                        type="text"
                        name="rokAkademicki"
                        value={kurs.rokAkademicki}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Klasa:</label>
                    <input
                        type="text"
                        name="klasa"
                        value={kurs.klasa}
                        onChange={handleChange}
                        required
                    />
                </div>
               {/* */}{/* Wyœwietlamy nazwisko u¿ytkownika */}{/*
                <div className="form-group">
                    <label>W³aœciciel:</label>
                    <input
                        type="text"
                        value={userName || 'Loading...'}
                        readOnly
                    />
                </div>*/}
                }
                <button type="submit" className="btn btn-primary">
                    Dodaj
                </button>
                }else
                {
                    <Link to="/kursy" className="btn btn-secondary">
                        Powrót do listy
                    </Link>
                }
            </form>
        </div>
    );
}

export default CreateKurs;
