import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import CharacteristicCreate from '../Create';
import CharacteristicUpdate from '../Update';

import TableComponent from '../../../../components/Table';
import TableCellComponent from '../../../../components/TableCell/TableCellComponent';
import TableCellActionComponent from '../../../../components/TableCell/TableCellActionComponent';

const CharacteristicTable = () => {
    const [loading, setLoading] = useState<boolean>(false);

    const { GetCharacteristics, DeleteCharacteristic } = useActions();
    const { characteristics } = useTypedSelector((store) => store.characteristic);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Characteristic";
            await GetCharacteristics();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCharacteristic(id);
        await GetCharacteristics();
    }

    return (
        <>
            <Container
                disableGutters
                maxWidth="lg"
                component="main"
                sx={{ pt: 8, pb: 6 }}>
                <CharacteristicCreate />
                <Paper>
                    <TableComponent
                        headLabel={["Id", "Name", "Characteristic", "Action"]}
                        rowsPerPageOptions={[5, 10, 25]}
                        itemsCount={characteristics.length}
                        bodyItems={characteristics
                            .map((row, index) => {
                                return [
                                    <TableCellComponent
                                        key={row.id}
                                        label={row.id}
                                    />,
                                    <TableCellComponent
                                        key={row.name}
                                        label={row.name}
                                    />,
                                    <TableCellComponent
                                        key={row.characteristicGroupName}
                                        label={row.characteristicGroupName}
                                    />,
                                    <TableCellActionComponent
                                        key={index}
                                        path={null}
                                        edit={<CharacteristicUpdate id={row.id} />}
                                        onDelete={() => onDelete(row.id)}
                                    />
                                ]
                            })
                        }
                    />
                </Paper>
            </Container>
        </>
    );
}

export default CharacteristicTable;