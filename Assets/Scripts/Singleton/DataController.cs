using LTAUnityBase.Base.DesignPattern;
public class DataController : Singleton<DataController>
{
    public FightersVO fightersVO;

    public TowersVO towersVO;

    public HeroesVO heroesVO;

    public SkillsVO skillsVO;

    public EffectBattlesVO effectBattlesVO;

    public void LoadLocalData()
    {
        fightersVO = new FightersVO();

        towersVO = new TowersVO();

        heroesVO = new HeroesVO();

        skillsVO = new SkillsVO();

        effectBattlesVO = new EffectBattlesVO();
    }
}
