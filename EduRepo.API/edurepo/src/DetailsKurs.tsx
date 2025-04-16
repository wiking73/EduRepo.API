
import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

interface Kurs {
    idKursu: number;
    nazwa: string;
    opisKursu: string;
    rokAkademicki: string;
    klasa: string;
    wlascicielUserName: string; 
}


function KursDetails() {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [formError, setFormError] = useState(null); // B³¹d zwi¹zany z formularzem
    const [kurs, setkurs] = useState<Kurs | null>(null);

    const { id } = useParams();
    const navigate = useNavigate();

    const fetchKurs = async () => {
        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${id}`);
            setkurs(response.data);
        } catch (err) {
            console.error('B³¹d', err);
           
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchKurs();

    
    }, [id]);

   

    if (isLoading) {
        return <p>£adowanie danych...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    if (!kurs) {
        return <p>Nie znaleziono kursu</p>;
    }

    return (
        <div className="bike-details">
            <h4>{kurs.nazwa}</h4>
            <h3>Szczegó³owe Informacje</h3>
            <p>Opis: {kurs.opisKursu}</p>
            <p>Klasa: {kurs.klasa}</p>
            <p>Rok akademicki: {kurs.rokAkademicki}</p>
           

            <Link to="/kursy" className="btn btn-secondary">
                Powrót do kursów
            </Link>

          
            <p>Stworzony przez: {kurs.wlascicielUserName}</p>

            

            
        </div>
    );
}

export default KursDetails;