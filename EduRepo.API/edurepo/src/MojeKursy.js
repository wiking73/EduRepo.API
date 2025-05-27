import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import './Styles/kursy.css';

const Odpowiedzi = () => {
    const [kursy, setKursy] = useState([]);

    const token = localStorage.getItem('authToken');
    const role = localStorage.getItem('role');
    const name = localStorage.getItem('displayName');

    const fetchKurs = async (id) => {
       
        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${id}`);         
            return response.data;          
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);
            return null;         
        } 
    };

    const fetchKursy = async (name) => {
        
        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${name}/mojekursy`);
            const courses = await Promise.all(
                response.data.map(k => fetchKurs(k.kursId))
            );
            setKursy(courses.filter(Boolean));           
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);       
        }
    };

    useEffect(() => {
        if(name) fetchKursy(name)
    }, [])

    return  (
        <div className="bike-grid">
            {kursy.map((kurs) => (
                <div className="bike-container" key={kurs.idKursu}>
                    <h3>{kurs.nazwa}</h3>
                    <div>
                        <Link to={`/details/${kurs.idKursu}`} className="bike-item-button">
                            Szczegóły
                        </Link>                      
                    </div>
             
            </div>))}
            
        </div>
    );
};

export default Odpowiedzi;
