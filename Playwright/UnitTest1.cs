using Microsoft.Playwright;

namespace Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    protected IPlaywright Playwright;
    protected IBrowser Browser;
    protected IBrowserContext Context;
    protected IPage Page;

    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.Locator("text=Get Started");

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    }

    [Test]
    public async Task DeveCriarPessoaComSucesso()
    {
        // Inicializa o Playwright e abre o navegador em modo não headless (mostra o navegador)
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,  // Mostra o navegador
            SlowMo = 500       // Atraso de 500ms entre cada ação (ajustável)
        });

        // Cria o contexto e a página
        Context = await Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
        // Acessar a página de criação de pessoa
        await Page.GotoAsync("http://localhost:5026/Pessoa/Create");

        // Preencher o formulário
        await Page.FillAsync("input[name='Nome']", "Igor");
        await Page.FillAsync("input[name='Email']", "igor@example.com");
        await Page.FillAsync("input[name='Enderecos[0].Rua']", "Rua 1");
        await Page.FillAsync("input[name='Enderecos[0].Cidade']", "Cidade X");
        await Page.FillAsync("input[name='Enderecos[0].Uf']", "Estado Y");

        // Enviar o formulário
        await Page.ClickAsync("button[type='submit']");

        // Verificar se foi redirecionado para a lista
        //Assert.IsTrue(Page.Url.Contains("/Pessoa"));
        Assert.IsNotNull(await Page.QuerySelectorAsync("text=Igor"));

        await Browser.CloseAsync();
        Playwright.Dispose();
    }
}
