import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TablePagination,
  Paper,
  TableFooter,
  Box,
  Grid,
  IconButton,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import theme from "~/theme/theme";
import AddButton from "~/components/buttons/AddButton";
import AddTransaction from "./transaction.add";
import DeleteTransaction from "./transaction.delete";
import type { Transaction } from "~/domain/transaction/transaction.type";
import { translateTransactionType } from "~/domain/mappers/mapper";

export default function TransactionPage(transactions: Transaction[]) {
  const [openAddTransaction, setOpenAddTransaction] = useState(false);
  const [openDeleteTransaction, setOpenDeleteTransaction] = useState(false);
  const [selectedTransactionId, setSelectedTransactionId] = useState<
    string | null
  >(null);

  const [rows, setRows] = useState<Transaction[]>([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [rowsTotal, setRowsTotal] = useState(0);

  const handleDeleteTransaction = (id: string) => {
    setSelectedTransactionId(id);
    setOpenDeleteTransaction(true);
  };

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  useEffect(() => {
    setRows(transactions ?? []);
    setRowsTotal(transactions?.length ?? 0);
  }, []);

  return (
    <>
      <AddTransaction
        open={openAddTransaction}
        onClose={() => setOpenAddTransaction(false)}
      />
      <DeleteTransaction
        open={openDeleteTransaction}
        onClose={() => setOpenDeleteTransaction(false)}
        id={selectedTransactionId!}
      />
      <Grid
        container
        spacing={2}
        sx={{
          width: "80%",
          marginTop: "1rem",
          marginLeft: "auto",
          marginRight: "auto",
          justifyContent: "flex-end",
        }}
      >
        <AddButton onClick={() => setOpenAddTransaction(true)} />
      </Grid>
      <Box
        sx={{
          width: "80%",
          marginTop: "2rem",
          marginLeft: "auto",
          marginRight: "auto",
        }}
      >
        <TableContainer
          component={Paper}
          sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}
        >
          <Table aria-label="agents table">
            <TableHead>
              <TableRow>
                <TableCell align="center">ID</TableCell>
                <TableCell>Descrição</TableCell>
                <TableCell align="center">Valor</TableCell>
                <TableCell align="center">Tipo</TableCell>
                <TableCell>Categoria</TableCell>
                <TableCell>Pessoa</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rowsTotal === 0 ? (
                <TableRow>
                  <TableCell colSpan={6} align="center">
                    Nenhuma transação encontrada
                  </TableCell>
                </TableRow>
              ) : (
                rows
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((row) => {
                    return (
                      <TableRow
                        key={row.id}
                        sx={{
                          "&:hover": { backgroundColor: "action.hover" },
                        }}
                      >
                        <TableCell align="center">{row.id}</TableCell>

                        <TableCell>{row.description}</TableCell>

                        <TableCell align="center">
                          {row.value.toLocaleString("pt-BR", {
                            style: "currency",
                            currency: "BRL",
                          })}
                        </TableCell>

                        <TableCell
                          align="center"
                          sx={{
                            fontWeight: 600,
                            color:
                              row.type === "Income"
                                ? "success.main"
                                : "error.main",
                            textTransform: "capitalize",
                          }}
                        >
                          {translateTransactionType(row.type)}
                        </TableCell>

                        <TableCell>
                          {row.category ? row.category.description : "—"}
                        </TableCell>

                        <TableCell>
                          {row.people ? row.people.name : "—"}
                        </TableCell>

                        <TableCell>
                          <IconButton
                            onClick={() => handleDeleteTransaction(row.id)}
                            aria-label="Deletar"
                            title="Deletar"
                          >
                            <DeleteIcon color="error" />
                          </IconButton>
                        </TableCell>
                      </TableRow>
                    );
                  })
              )}
            </TableBody>

            <TableFooter>
              {rows.length === 0 && (
                <TableRow>
                  <TableCell colSpan={3} />
                </TableRow>
              )}
            </TableFooter>
          </Table>
          <TablePagination
            rowsPerPageOptions={[3, 5, 10]}
            component="div"
            count={rowsTotal}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
            labelRowsPerPage="Resultados por página"
            labelDisplayedRows={({ from, to, count }) =>
              `${from}-${to} de ${count !== -1 ? count : `mais de ${to}`}`
            }
          />
        </TableContainer>
      </Box>
    </>
  );
}