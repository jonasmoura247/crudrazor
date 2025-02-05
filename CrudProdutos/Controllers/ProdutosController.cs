using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudProdutos.Data;
using CrudProdutos.Models;

namespace CrudProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Inst�ncia do banco de dados.</param>

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Exibe a lista de produtos dispon�veis.
        /// </summary>
        /// <returns>Uma View com a lista de produtos.</returns>
        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produtos.Where(p => p.Quantidade > 0).ToListAsync();
            return View(produtos);
        }
        /// <summary>
        /// Exibe os detalhes de um produto espec�fico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Uma View com os detalhes do produto.</returns>
        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        /// <summary>
        /// Retorna a View para criar um novo produto.
        /// </summary>
        /// <returns>Uma View para cria��o de um produto.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processa o envio do formul�rio para cria��o de um novo produto.
        /// </summary>
        /// <param name="produto">Objeto produto preenchido pelo usu�rio.</param>
        /// <returns>Redireciona para a p�gina inicial ou retorna a View caso haja erro.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Quantidade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        /// <summary>
        /// Retorna a View para edi��o de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser editado.</param>
        /// <returns>Uma View para edi��o do produto.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        /// <summary>
        /// Processa a edi��o de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="produto">Objeto produto atualizado.</param>
        /// <returns>Redireciona para a lista de produtos ou retorna a View caso haja erro.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Quantidade")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExiste(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        /// <summary>
        /// Retorna a View de confirma��o para deletar um produto.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Uma View para confirmar a exclus�o.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        /// <summary>
        /// Processa a exclus�o de um produto do banco de dados.
        /// </summary>
        /// <param name="id">ID do produto a ser exclu�do.</param>
        /// <returns>Redireciona para a p�gina inicial.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Verifica se um produto existe no banco de dados.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>True se o produto existir, False caso contr�rio.</returns>
        private bool ProdutoExiste(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
