import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import CharacteristicGroupCreate from '../Create';
import CharacteristicGroupUpdate from '../Update';

import TableComponent from '../../../../components/Table';
import TableCellComponent from '../../../../components/TableCell/TableCellComponent';
import TableCellActionComponent from '../../../../components/TableCell/TableCellActionComponent';

const CharacteristicGroupTable = () => {
    const [loading, setLoading] = useState<boolean>(false);

    const { GetCharacteristicGroups, DeleteCharacteristicGroup } = useActions();
    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Characteristic Group";
            await GetCharacteristicGroups();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCharacteristicGroup(id);
        getData();
    }

    return (
        <Container
            disableGutters
            maxWidth="lg"
            component="main"
            sx={{ pt: 8, pb: 6 }}>
            <CharacteristicGroupCreate />
            <Paper>
                <TableComponent
                    headLabel={["Id", "Name", "Action"]}
                    rowsPerPageOptions={[1, 5, 10, 25]}
                    itemsCount={characteristicGroups.length}
                    bodyItems={characteristicGroups
                        .map((row, index) => {
                            return [
                                <TableCellComponent
                                    key={row.id}
                                    label={row.id} />,
                                <TableCellComponent
                                    key={row.name}
                                    label={row.name} />,
                                <TableCellActionComponent
                                    key={index}
                                    path={null}
                                    edit={<CharacteristicGroupUpdate id={row.id} />}
                                    onDelete={() => onDelete(row.id)}
                                />
                            ]
                        })
                    }
                />
            </Paper>
        </Container>
    );
}

export default CharacteristicGroupTable