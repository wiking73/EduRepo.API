import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';


const token = localStorage.getItem('authToken');
function CreateKurs() {
    const [kurs, setKurs] = useState({
        idKursu: "",
        nazwa: "",
        opisKursu: "",
        rokAkademicki: "",
        klasa: "",
        wlascicielUserName: "", 
    });





    const navigate = useNavigate();

    const handleChange = (e: { target: { name: any; value: any; type: any; checked: any; }; }) => {
        const { name, value, type, checked } = e.target;

        let newValue = value;
        if (type === 'textbox') {
            newValue = value;
        }
        else if (type === 'checkbox') {
            newValue = checked;
        } else if (type === 'number') {
            newValue = value === '' ? '' : parseFloat(value);
        }

        setKurs({
            ...kurs,
            [name]: newValue,
        });
    };

    const handleSubmit = (e: { preventDefault: () => void; }) => {
        e.preventDefault();

        if (!kurs.nazwa) {
            alert('Nazwa jest wymagana');
            return;
        }
        if (!kurs.opisKursu) {
            alert('Rozmiar jest wymagany');
            return;
        }
        if (!kurs.rokAkademicki) {
            alert('Typ roweru jest wymagany');
            return;
        }
        if (!kurs.klasa) {
            alert('Stawka godzinowa musi byæ wiêksza ni¿ 0');
            return;
        }


        const kursToSend = {
            ...kurs,
            Nazwa: kurs.nazwa,
            opisKursu: kurs.opisKursu,
            rokAkademicki: kurs.rokAkademicki,
            klasa: kurs.klasa,
        };

        console.log('Wysy³ane dane roweru:', kursToSend);

        const token = localStorage.getItem('authToken'); 

        axios
            .post('https://localhost:7157/api/Kurs', kursToSend, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
            .then(() => {
                navigate('/');
            })
            .catch((error) => {
                console.error('B³¹d podczas tworzenia roweru:', error);
                if (error.response && error.response.data) {
                    alert(`B³¹d: ${error.response.data}`);
                } else {
                    alert('Wyst¹pi³ b³¹d podczas tworzenia roweru.');
                }
            });

    };

    return (
        <div className="bike-form">
            <h6>Dodaj nowy kurs</h6>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Nazwa:</label>
                    <input type="text" name="name" value={kurs.nazwa} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label>Rozmiar:</label>
                    <input type="text" name="name" value={kurs.opisKursu} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label>Typ roweru:</label>
                    <input type="text" name="name" value={kurs.rokAkademicki} onChange={handleChange} required />
                </div>
                
                
                <div className="form-group">
                    <label>Stawka godzinowa (z³):</label>
                    <input type="text" name="name" value={kurs.klasa} onChange={handleChange} required />
                </div>
                

                <button type="submit" className="btn btn-primary">
                     Dodaj 
                </button>
                <Link to="/kursy" className="btn btn-secondary">
                    Powrót do listy 
                </Link>
            </form>
        </div>
    );
}

export default CreateKurs;