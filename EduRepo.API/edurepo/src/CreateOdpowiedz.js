import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate, Link } from 'react-router-dom';
import "./Styles/CreateZadanie.css";
;
//const role = localStorage.getItem('role');
function CreateOdpowiedz() {
    const { id, IdZadania } = useParams();
    const token = localStorage.getItem('authToken')

    const navigate = useNavigate();
    const [error, setError] = useState(null);
    const [userId, setUserId] = useState(null);
    const [unique_name, setUserName] = useState(null);
    const [file, setFile] = useState(null);
    const [zadanie, setZadanie] = useState(null)

    const [odpowiedz, setOdpowiedz] = useState({
        "idOdpowiedzi": "",
        "IdZadania": "",
        "wlascicielId": "",
        "userName": "",
        "dataOddania": Date.now(),
        "komentarzNauczyciela": "Brak",
        "nazwaPliku": "",
        "ocena": "Brak"

    });



    useEffect(() => {
        fetchUserData();
        fetchZadanie(IdZadania);
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
            alert("Nie uda³o siê odczytaæ UserId z tokena.");
            return;
        }
        setUserId(userData.nameid);
        setUserName(userData.unique_name);
    };

    const fetchZadanie = async (idZadania) => {
        try {
            if (!token) {
                setError('Musisz byæ zalogowany!');
                return;
            }
            const response = await axios.get(`https://localhost:7157/api/Zadanie/${idZadania}`, {
                headers: { Authorization: `Bearer ${token}` },
            })
            setZadanie(response.data)
        } catch (err) {
            console.error('B³¹d pobierania zadania:', err);
        }
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;

        setOdpowiedz(prev => ({
            ...prev,
            [name]: newValue,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!file) return;

        const odpowiedzToSend = {
            idZadania: IdZadania,
            nazwaPliku: odpowiedz.nazwaPliku,
            dataOddania: new Date().toISOString(),
            komentarzNauczyciela: "brak",
            ocena: "brak",
            userId: userId,
            name: unique_name,
        };

        const formData = new FormData();
        formData.append("IdZadania", IdZadania);
        formData.append("UserId", userId);
        formData.append("Name", unique_name);
        formData.append("DataOddania", new Date().toISOString());
        formData.append("KomentarzNauczyciela", "brak");
        formData.append("NazwaPliku", file.name);
        formData.append("Ocena", "brak");
        formData.append("file", file);
        console.log(file.name)
        //for (const [key, value] of formData.entries()) {
        //    console.log(key, value);
        //}

        axios.post('https://localhost:7157/api/Odpowiedz', formData, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(() => {
                alert("Dodano zadanie");
                navigate(`/details/${id}`)
            })
            .catch((error) => {
                console.error('B³¹d podczas dodania Odpowiedzi:', error);
                alert('Wyst¹pi³ b³¹d podczas dodania Odpowiedz.');
                console.error('Szczegó³y b³êdu:', error.response.data);
            });
    };

    return (
        <div className="form-container">
            <h4>Dodaj Odpowiedz</h4>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>{zadanie?.nazwa}</label>
                    {/*<input*/}
                    {/*    type="text"*/}
                    {/*    name="nazwaPliku"*/}
                    {/*    value={odpowiedz.nazwaPliku}*/}
                    {/*    onChange={handleChange}*/}
                    {/*    required*/}
                    {/*/>*/}
                </div>
                <input
                    type="file"
                    onChange={(e) => setFile(e.target.files[0])} required />

                {/*<div className="form-group">*/}
                {/*    <label>Treœæ:</label>*/}
                {/*    <textarea*/}
                {/*        name="tresc"*/}
                {/*        value={zadanie.tresc}*/}
                {/*        onChange={handleChange}*/}
                {/*        required*/}
                {/*    />*/}
                {/*</div>*/}






                <button type="submit" className="btn btn-primary">Dodaj</button>
                <Link to={`/details/${id}`} className="btn btn-secondary">Anuluj</Link>
            </form>
        </div>
    );
}

export default CreateOdpowiedz;
