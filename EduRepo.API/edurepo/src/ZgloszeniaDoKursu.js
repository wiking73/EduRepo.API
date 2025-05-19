import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import { Button, List } from 'semantic-ui-react';

function ZgloszeniaDoKursu() {
    const { id } = useParams(); // id kursu
    const [zgloszenia, setZgloszenia] = useState([]);
    const [error, setError] = useState(null);

    const fetchZgloszenia = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(`https://localhost:7157/api/Kurs/${id}/prosby`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setZgloszenia(response.data);
        } catch (err) {
            setError("Błąd podczas pobierania zgłoszeń.");
        }
    };

    const handleAkceptuj = async (uczId) => {
        const token = localStorage.getItem('authToken');
        await axios.put(`https://localhost:7157/api/Kurs/${id}/zaakceptuj/${uczId}`, {}, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        fetchZgloszenia(); // odświeżenie listy
    };

    const handleOdrzuc = async (uczId) => {
        const token = localStorage.getItem('authToken');
        await axios.put(`https://localhost:7157/api/Kurs/${id}/odrzuc/${uczId}`, {}, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        fetchZgloszenia(); // odświeżenie listy
    };

    useEffect(() => {
        fetchZgloszenia();
    }, [id]);

    return (
        <div className="kurs-details">
            <h3>Oczekujące zgłoszenia do kursu</h3>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {zgloszenia.length === 0 ? (
                <p>Brak oczekujących zgłoszeń.</p>
            ) : (
                <List divided>
                    {zgloszenia.map(z => (
                        <List.Item key={z.idUczestnictwa}>
                            <List.Content>
                                <List.Header>{z.userName}</List.Header>
                                <Button color="green" onClick={() => handleAkceptuj(z.idUczestnictwa)}>Akceptuj</Button>
                                <Button color="red" onClick={() => handleOdrzuc(z.idUczestnictwa)}>Odrzuć</Button>
                            </List.Content>
                        </List.Item>
                    ))}
                </List>
            )}
            <Link to={`/details/${id}`} className="btn btn-secondary">Powrót do kursu</Link>
        </div>
    );
}

export default ZgloszeniaDoKursu;
