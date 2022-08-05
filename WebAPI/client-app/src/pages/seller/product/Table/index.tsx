import { Avatar, Box, Checkbox, IconButton, TableRow, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { IProductInfo } from '../types';
import { HeadCell } from '../../../../store/types';

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';
import LinkRouter from '../../../../components/LinkRouter';
import { white_plus } from '../../../../assets/icons';


const headCells: HeadCell<IProductInfo>[] = [
    {
        id: 'id',
        numeric: true,
        label: 'Identifier',
    },
    {
        id: 'name',
        numeric: false,
        label: 'Name',
    },
    {
        id: 'image',
        numeric: false,
        label: 'Image',
    },
    {
        id: 'price',
        numeric: false,
        label: 'Price',
    },
    {
        id: 'count',
        numeric: false,
        label: 'Count',
    },
    {
        id: 'statusName',
        numeric: false,
        label: 'Status Name',
    },
    {
        id: 'categoryName',
        numeric: false,
        label: 'Category Name',
    }
];

const ProductTable = () => {
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IProductInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchProducts, DeleteProducts } = useActions();
    const { products, count } = useTypedSelector((store) => store.productSeller);

    useEffect(() => {
        document.title = "Products";
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchProducts(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteProducts(selected);
        setPage(1);
    }

    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const selectedIndex = selected.indexOf(id);
        let newSelected: readonly number[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, id);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1),
            );
        }

        setSelected(newSelected);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (selected.length == 0) {
            const newSelecteds = products.map((n) => n.id);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const isSelected = (id: number) => selected.indexOf(id) !== -1;

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(1);
    };

    return (
        <>
            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", my: "30px" }}>
                <Typography variant="h1">Products</Typography>
                <LinkRouter underline="none" to="/seller/product/create" >
                    <IconButton
                        sx={{ borderRadius: '12px', background: "#F45626", "&:hover": { background: "#CB2525" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        color="inherit"
                    >
                        <img
                            style={{ width: "30px" }}
                            src={white_plus}
                            alt="icon"
                        />
                    </IconButton>
                </LinkRouter>
            </Box>
            <EnhancedTable
                page={page}
                rowsPerPage={rowsPerPage}
                handleChangePage={handleChangePage}
                handleChangeRowsPerPage={handleChangeRowsPerPage}
                setName={setName}
                handleSelectAllClick={handleSelectAllClick}
                isAscOrder={isAscOrder}
                setIsAscOrder={setIsAscOrder}
                orderBy={orderBy}
                setOrderBy={setOrderBy}
                headCells={headCells}
                numSelected={selected.length}
                count={count}
                show={null}
                update={null}
                onDelete={onDelete}
                tableBody={products.map((row, index) => {
                    const isItemSelected = isSelected(row.id);
                    const labelId = `enhanced-table-checkbox-${index}`;

                    return (
                        <TableRow
                            hover
                            onClick={(event) => handleClick(event, row.id)}
                            role="checkbox"
                            aria-checked={isItemSelected}
                            tabIndex={-1}
                            key={row.id}
                            selected={isItemSelected}
                        >
                            <TableCellStyle sx={{ paddingLeft: "11px" }} padding="checkbox">
                                <Checkbox
                                    color="primary"
                                    sx={{ borderRadius: "12px" }}
                                    checked={isItemSelected}
                                    inputProps={{
                                        'aria-labelledby': labelId,
                                    }} />
                            </TableCellStyle>
                            <TableCellStyle
                                component="th"
                                id={labelId}
                                scope="row"
                            >
                                {row.id}
                            </TableCellStyle>
                            <TableCellStyle align="center">{row.name}</TableCellStyle>
                            <TableCellStyle align="center">
                                <Avatar
                                    sx={{ mx: "auto" }}
                                    src={row.image}
                                />
                            </TableCellStyle>
                            <TableCellStyle align="center">{row.price}</TableCellStyle>
                            <TableCellStyle align="center">{row.count}</TableCellStyle>
                            <TableCellStyle align="center">{row.statusName}</TableCellStyle>
                            <TableCellStyle align="center">{row.categoryName}</TableCellStyle>
                        </TableRow>
                    );
                })} />
        </>
    );
}

export default ProductTable