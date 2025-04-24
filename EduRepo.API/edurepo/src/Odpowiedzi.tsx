import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import './Styles/kursy.css';

import { Menu, Button } from 'semantic-ui-react';

interface Odpowiedz {
    idOdpowiedzi: number;

        
        dataOddania: Date,
        komentarzNauczyciela: string,
        nazwaPliku: string,
        ocena: string,
}

const Odpowiedzi: React.FC = () => {
    const [odpowiedzi, setodpowiedzi] = useState<Odpowiedz[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const token = localStorage.getItem('authToken');
    const role = localStorage.getItem('role');


    useEffect(() => {
        fetchOdpowiedzi();
    }, []);

    const fetchOdpowiedzi = async () => {
        setLoading(true);
        try {
            const response = await axios.get<Odpowiedz[]>('https://localhost:7157/api/Odpowiedz');
            setodpowiedzi(response.data);
        } catch (err: any) {
            console.error('Błąd pobierania listy kursow:', err);
            setError('Nie udało się pobrać listy kursow.');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteOdpowiedz = async (idOdpowiedzi: number) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }

            if (window.confirm('Czy na pewno chcesz usunąć ten rower?')) {
                await axios.delete(`https://localhost:7157/api/Odpowiedz/${idOdpowiedzi}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setodpowiedzi((prev) => prev.filter((b) => b.idOdpowiedzi !== idOdpowiedzi));
            }
        } catch (err: any) {
            console.error('Błąd', err);

        }
    };


    const handleEditOdpowiedz = (idodpowiedz: number) => {
        console.log(`Edytuj kurs o ID: ${idodpowiedz} (tylko Admin)`);
    };

    if (loading) {
        return <p>Ładowanie danych...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    return (

        <div className="bike-list-background">

            <div className="bike-list-container">
                {/* Menu dla Admina */}




                {odpowiedzi.length > 0 ? (
                    <div className="bike-grid">
                        {odpowiedzi.map((odpowiedz) => (
                            <div className="bike-container" key={odpowiedz.idOdpowiedzi}>


                                {odpowiedz.nazwa}


                                <div>
                                    <Link to={`/details/${odpowiedz.idOdpowiedzi}`} className="bike-item-button">
                                        Szczegóły
                                    </Link>

                                    <>
                                        <Link to={`/edit/${odpowiedz.idOdpowiedzi}`} className="edit-button">
                                            Edytuj
                                        </Link>
                                        <button
                                            onClick={() => handleDeleteOdpowiedz(odpowiedz.idOdpowiedzi)}
                                            className="bike-item-button2"
                                        >
                                            Usuń
                                        </button>
                                        {/* Nie zrobione*/}
                                        
                                    </>

                                </div>
                            </div>

                        ))}
                    </div>
                ) : (
                    <p>Brak rowerów do wyświetlenia.</p>
                )}
            </div>

        </div>
    );

};

export default Odpowiedzi;