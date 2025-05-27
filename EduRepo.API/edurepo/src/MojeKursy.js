import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import './Styles/kursy.css';

const Odpowiedzi = () => {
    const fetchKursy = async () => {
        
        try {
            const response = await axios.get('https://localhost:7157/api/Kurs');
            
            console.log(response.data)
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);
           
        } finally {
            
        }
    };

    return  (
        <div className="bike-list-background">
           
        </div>
    );
};

export default Odpowiedzi;
