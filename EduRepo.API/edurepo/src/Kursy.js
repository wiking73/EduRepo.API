import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import './Styles/kursy.css';
import { Menu, Button } from 'semantic-ui-react';

const Kursy = () => {
    const [mojeKursy, setMojeKursy] = useState([]);
    const [wszystkieKursy, setWszystkieKursy] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    const token = localStorage.getItem('authToken');
    const role = localStorage.getItem('role');
    const name = localStorage.getItem('displayName');

    useEffect(() => {
        fetchKursy();
        fetchMojeKursy(name);
        
    }, []);
    
    const fetchMojeKursy = async (name) => {

        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${name}/mojekursy`);         
            const ids = response.data.map((k) => k.kursId)
            setMojeKursy(ids)
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);
        }
    };

    const fetchKursy = async () => {
        setLoading(true);
        try {
            const response = await axios.get('https://localhost:7157/api/Kurs', {
                headers: { Authorization: `Bearer ${token}` }
            });
            setWszystkieKursy(response.data);         
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);
            setError('Nie udało się pobrać listy kursów.');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteKurs = async (idKursu) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }

            if (window.confirm('Czy na pewno chcesz usunąć ten kurs?')) {
                await axios.delete(`https://localhost:7157/api/Kurs/${idKursu}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setWszystkieKursy((prev) => prev.filter((b) => b.idKursu !== idKursu));
            }
        } catch (err) {
            console.error('Błąd', err);
        }
    };

    const handleAddKurs = () => {
        console.log('Dodaj kurs (tylko Admin)');
    };

    const handleEditKurs = (idKursu) => {
        console.log(`Edytuj kurs o ID: ${idKursu} (tylko Admin)`);
    };

    const handleZapiszKurs = async (idKursu) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }


            const response = await axios.post(
                `https://localhost:7157/api/Kurs/${idKursu}/dolacz`,
                {},
                {
                    headers: { Authorization: `Bearer ${token}` },
                }
            );

            if (response.status === 200) {
                alert('Wysłano prośbę o dołączenie do kursu.');
            }
        } catch (error) {
            console.error('Błąd zapisywania na kurs:', error.response ? error.response.data : error.message);
        }
    };

    if (loading) {
        return <p>�adowanie danych...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    return (
        <div className="bike-list-background">
            <div className="bike-list-container">
                <div className="bike-list__header">
                    {role === 'Teacher' && (
                        <Menu.Item as={NavLink} to="/kurs/create">
                            <Button content="Dodaj Kurs" size="large" className="custom-button17" onClick={handleAddKurs} />
                        </Menu.Item>
                    )}
                    <Menu.Item as={NavLink} to="/bikes/filtersort">
                        <Button content="Filtrowanie/Sortowanie" size="large" className="custom-button18" />
                    </Menu.Item>
                </div>

                {wszystkieKursy.length > 0 ? (
                    <div className="bike-grid">
                        {wszystkieKursy.map((kurs) => (
                            <div className="bike-container" key={kurs.idKursu}>
                                <h3>{kurs.nazwa}</h3>
                                <div>
                                    <Link to={`/details/${kurs.idKursu}`} className="bike-item-button">
                                        Szczegóły
                                    </Link>
                                    {role !== 'Student' && (
                                        <>
                                            <Link to={`/edit/${kurs.idKursu}`} className="edit-button">
                                                Edytuj
                                            </Link>
                                            <button onClick={() => handleDeleteKurs(kurs.idKursu)} className="bike-item-button2">
                                                Usuń
                                            </button>
                                        </>              
                                    )}                                                            
                                </div>

                                <div>           
                                    {role === 'Student' && (
                                        mojeKursy.includes(kurs.idKursu) ? <p>Jesteś uczestnikiem kursu</p> : 
                                            <button onClick={() => handleZapiszKurs(kurs.idKursu)} className="bike-item-button2">
                                                Zapisz Mnie
                                            </button>                                        
                                    )}
                                </div>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>Brak kursów do wyświetlenia.</p>
                )}
            </div>
        </div>
    );
};

export default Kursy;
