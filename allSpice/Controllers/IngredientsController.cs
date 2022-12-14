namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;

  private readonly IngredientsService _is;

  public IngredientsController(Auth0Provider auth0provider, IngredientsService @is)
  {
    _auth0provider = auth0provider;
    _is = @is;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Ingredient>> PostIngredient([FromBody] Ingredient iData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      iData.CreatorId = userInfo.Id;
      Ingredient newI = _is.PostIngredient(iData);
      return Ok(newI);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{IngId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteIngredient(int IngId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      // TODO how can I pass in the userInfo Id here?
      _is.DeleteIngredient(IngId);
      return Ok("Ingredient Removed");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
