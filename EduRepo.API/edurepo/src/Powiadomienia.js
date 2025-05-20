import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { List, Message, Icon } from 'semantic-ui-react';

function Powiadomienia() {
    const [powiadomienia, setPowiadomienia] = useState([]);
    const [error, setError] = useState(null);

    const fetchPowiadomienia = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get("https://localhost:7157/api/Powiadomienia/dla-nauczyciela", {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setPowiadomienia(response.data);
        } catch (err) {
            console.error("Błąd przy pobieraniu powiadomień", err);
            setError("Nie udało się pobrać powiadomień.");
        }
    };

    useEffect(() => {
        fetchPowiadomienia();
    }, []);

    if (error) {
        return <Message negative>{error}</Message>;
    }

    if (powiadomienia.length === 0) {
        return <p style={{ margin: '1rem' }}>Brak nowych powiadomień.</p>;
    }

    return (
        <div style={{ margin: '1rem' }}>
            <h4><Icon name="bell" /> Powiadomienia</h4>
            <List divided relaxed>
                {powiadomienia.map(p => (
                    <List.Item key={p.idPowiadomienia}>
                        <List.Icon name="user plus" size="large" verticalAlign="middle" />
                        <List.Content>
                            <List.Description>
                                Użytkownik chce dołączyć do kursu (ID zadania: {p.idZadania})<br />
                                <small>{new Date(p.dataPowiadomienia).toLocaleString()}</small>
                            </List.Description>
                        </List.Content>
                    </List.Item>
                ))}
            </List>
        </div>
    );
}

export default Powiadomienia;
